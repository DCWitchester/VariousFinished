using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LocationDisplay.Miscellaneous
{
    public class Encryption
    {
        private TripleDESCryptoServiceProvider cryptoService = new TripleDESCryptoServiceProvider();
        private UTF8Encoding encoding = new UTF8Encoding();
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
        // the procedure exists to alter a base ByteArray
        public Byte[] Transform(Byte[] input, ICryptoTransform cryptoTransform)
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
    }
}
