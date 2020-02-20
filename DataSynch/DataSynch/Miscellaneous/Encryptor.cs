using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataSynch.Miscellaneous
{
    /// <summary>
    /// the class for Data Encryption/Decryption
    /// </summary>
    class Encrypter_Old
    {
        /// <summary>
        /// the main encryptionServiceProvider
        /// </summary>
        private TripleDESCryptoServiceProvider cryptoService = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// the main encoding
        /// </summary>
        private UTF8Encoding encoding = new UTF8Encoding();

        ///<summary>
        /// the procedure exists to alter a base ByteArray
        /// </summary>
        private Byte[] Transform(Byte[] input, ICryptoTransform cryptoTransform)
        {
            if (input.Length <= 0) return new Byte[] { 0 };

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Position = 0;
            Byte[] result = memoryStream.ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            return result;
        }
        /// <summary>
        /// the function used for encrypting a given string
        /// </summary>
        /// <param name="text">the given string</param>
        /// <returns>an encryted text</returns>
        public String Encrypt(String text)
        {
            #region key and vector lenght generating 
            Byte[] key = new Byte[24];
            Byte[] lenghtVector = new Byte[8];
            RNGCryptoServiceProvider randomGenerator = new RNGCryptoServiceProvider();
            randomGenerator.GetBytes(key);
            randomGenerator.GetBytes(lenghtVector);
            cryptoService.Key = key;
            cryptoService.IV = lenghtVector;
            #endregion

            return Convert.ToBase64String(cryptoService.Key) + Convert.ToBase64String(Transform(encoding.GetBytes(text), cryptoService.CreateEncryptor(cryptoService.Key, cryptoService.IV))) +
                    Convert.ToBase64String(cryptoService.IV);
        }
        /// <summary>
        /// the function used for decrypting a given string
        /// </summary>
        /// <param name="encryptedText">the already encrypted text</param>
        /// <returns>the original text</returns>
        public String Decrypt(String encryptedText)
        {
            try
            {
                return encoding.GetString(Transform(Convert.FromBase64String(encryptedText.Substring(32, encryptedText.Length - 44)),
                        cryptoService.CreateDecryptor(Convert.FromBase64String(encryptedText.Substring(0, 32)),
                        Convert.FromBase64String(encryptedText.Substring(encryptedText.Length - 12)))));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    class Encrypter
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
