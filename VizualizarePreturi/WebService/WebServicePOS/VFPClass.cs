using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WebServiceBergenbier;

namespace WebServiceEvidenta
{
    public class VFPClass
    {
        public class vfpPOS : IDisposable
        {
            
            static String EvidentaPath = Decryptor.Decrypt(Properties.Settings.Default.CaleEvidenta);
            static String odbcCheck = EvidentaPath + @"\FOXUSER.DBF";
            static String dbfPath = EvidentaPath + @"\DBF";
            static String nomPath = EvidentaPath + @"\NOM";
            System.Data.OleDb.OleDbConnection dbfConn = new System.Data.OleDb.OleDbConnection();
            System.Data.OleDb.OleDbCommand oCmd;
            string eroare = "";
            string NomenclatorProduse = nomPath + @"\FP.DBF";

            //
            public string newLine = "\r\n";
            public string fontBold = ((char)27).ToString() + ((char)33).ToString() + ((char)10).ToString();
            public string fontBoldMare = ((char)27).ToString() + ((char)33).ToString() + ((char)32).ToString();
            public string fontNormal = ((char)27).ToString() + ((char)33).ToString() + ((char)0).ToString();

            /* IDisposable */
            private bool disposed = false;
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed resources                    
                    }
                    // Call the appropriate methods to clean up unmanaged resources here
                    dbfConn.Close();
                    disposed = true;
                }
            }
            ~vfpPOS()
            {
                Dispose(false);
            }
            /* END IDisposable */

            public vfpPOS()
            {
                #region initializare vfpoledb
                dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + EvidentaPath + ";Collating Sequence=general;";
                String x = "";
                try
                {
                    dbfConn.Open();
                }
                catch (Exception ee)
                {
                    //eroare = "Eroare: nu ma pot conecta la baza de date pentru comunicare: " + fisVanzari + " " + ee.Message;
                    // trebuie sa si trimit eroarea asta la o pagina de facut Erori.aspx?text=eroare;
                    return;
                }
                oCmd = dbfConn.CreateCommand();
                oCmd.CommandText = "SET EXCLUSIVE OFF";
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = "SET DELETED ON";            // excludem inreg. marcate pt stergere
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = "SET NULL OFF";              // permitem valori NULL pt. update,insert....
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = "SET ENGINEBEHAVIOR 70";     // pentru clauza GROUP BY: sa permita gruparea fara a specifica toara campurile ( de la 80: GROUP BY clause must list every field in the SELECT list except for fields contained in an aggregate function)
                oCmd.ExecuteNonQuery();
                #endregion
                //oCmd.CommandText = @"SELECT * FROM " + fisVanzari;
            }
            #region ProductDisplay
          
            public XmlDocument getProductDetails(String productCode)
            { 
                String command = String.Format("SELECT TOP 1 denm,pv FROM '{0}' WHERE UPPER(ALLTRIM(codp)) == '{1}' ORDER BY codp",
                                                    NomenclatorProduse,
                                                    productCode.ToUpper().Trim());
                oCmd.CommandText = command;
                DataTable dt = new DataTable();
                try
                {
                    dt.Load(oCmd.ExecuteReader());
                } catch { }
                dt.TableName = "Produs";
                Classes.ProductDisplay productDisplay = new Classes.ProductDisplay();
                productDisplay.GetProductDisplayFromDataTable(dt);
                XmlSerializer serializer = new XmlSerializer(typeof(Classes.ProductDisplay));
                String result = String.Empty;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    serializer.Serialize(memoryStream, productDisplay);
                    memoryStream.Position = 0;
                    result = new StreamReader(memoryStream).ReadToEnd();
                }
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(result);
                return xmlDocument;
            }
            #endregion 
        }
    }
    public class Decryptor
    {
        /// <summary>
        /// the main encryptionServiceProvider
        /// </summary>
        private static TripleDESCryptoServiceProvider cryptoService = new TripleDESCryptoServiceProvider();

        /// <summary>
        /// the main encoding
        /// </summary>
        private static UTF8Encoding encoding = new UTF8Encoding();

        ///<summary>
        /// the procedure exists to alter a base ByteArray
        /// </summary>
        private static Byte[] Transform(Byte[] input, ICryptoTransform cryptoTransform)
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
        public static String Encrypt(String text)
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
        public static String Decrypt(String encryptedText)
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
}