using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebServicePOS.Auxiliary
{
    public class Codificator
    {
        /// <summary>
        /// the private encryption service provider
        /// </summary>
        private readonly TripleDESCryptoServiceProvider x3des = new TripleDESCryptoServiceProvider();
        /// <summary>
        /// the UTF8 base Encoding
        /// </summary>
        private readonly UTF8Encoding xutf8 = new UTF8Encoding();

        /// <summary>
        /// this function will encrypt a given text
        /// </summary>
        /// <param name="text">the given text</param>
        /// <returns>the encrypted text</returns>
        public string Encrypt(string text)
        {
            # region generare cheie si vector de lungimi cunoscute
            // ma asigur ca key si iv au exact lungimile 24 respectiv 8
            byte[] key = new byte[24];
            byte[] iv = new byte[8];
            RNGCryptoServiceProvider randgen = new RNGCryptoServiceProvider();
            randgen.GetBytes(key);
            randgen.GetBytes(iv);
            x3des.Key = key;
            x3des.IV = iv;

            // tot codul de mai sus e semiinutil , dar acum nu mai depind de extensii viitoare ale clasei 
            //TripleDESCryptoServiceProvider ( care ar putea modifica 24 si 8)
            #endregion
            return Convert.ToBase64String(x3des.Key) + Convert.ToBase64String(Transform(xutf8.GetBytes(text),
                                          x3des.CreateEncryptor(x3des.Key, x3des.IV))) + Convert.ToBase64String(x3des.IV);
        }

        /// <summary>
        /// this function will decrypt the given text
        /// </summary>
        /// <param name="text">the given text</param>
        /// <returns>the decrypted text</returns>
        public string Decrypt(string text)
        {
            try
            {
                return xutf8.GetString(Transform(Convert.FromBase64String(text.Substring(32, text.Length - 44)),
                       x3des.CreateDecryptor(Convert.FromBase64String(text.Substring(0, 32)),
                       Convert.FromBase64String(text.Substring(text.Length - 12)))));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            if (input.Length > 0)
            {
                MemoryStream memStream = new MemoryStream();
                CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
                cryptStream.Write(input, 0, input.Length);
                cryptStream.FlushFinalBlock();
                memStream.Position = 0;
                byte[] result = memStream.ToArray();
                memStream.Close();
                cryptStream.Close();
                return result;
            }
            else
            { return new byte[] { 0 }; }
        }
    }
}