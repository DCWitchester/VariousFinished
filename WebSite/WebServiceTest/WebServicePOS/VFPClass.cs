using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebServicePOS
{
    public class VFPClass
    {
        public class vfpPOS : IDisposable
        {
            System.Data.OleDb.OleDbConnection dbfConn = new System.Data.OleDb.OleDbConnection();
            System.Data.OleDb.OleDbCommand oCmd;
            string eroare = "";
            string fisDBC = Setari.calePOS + @"\v.dbc";
            string fisVanzari = Setari.calePOS + @"\vanz.dbf";
            string fisPers = Setari.calePOS + @"\pers.dbf";
            string fisGest = Setari.calePOS + @"\gest.dbf";
            string fisCateg = Setari.calePOS + @"\categ.dbf";
            string fisProd = Setari.calePOS + @"\prod.dbf";
            string fisMese = Setari.calePOS + @"\mese.dbf";
            string fisSetari = Setari.calePOS + @"\setari.dbf";
            string fisIstoric = Setari.calePOS + @"\istoric.dbf";
            string fisFeluri = Setari.calePOS + @"\feluri.dbf";
            string fisTipProd = Setari.calePOS + @"\tipprod.dbf";
            string fisJSONVANZ = Setari.calePOS + @"\jsonvanz.dbf";
            string fisCoteTva = Setari.calePOS + @"\cote_tva.dbf";
            string fisStariVanzari = Setari.calePOS + @"\stari_vanz.dbf";

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
                dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + Setari.calePOS + ";Collating Sequence=general;";
                dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + fisDBC + ";Collating Sequence=general;";
                try
                {
                    dbfConn.Open();
                }
                catch (Exception ee)
                {
                    eroare = "Eroare: nu ma pot conecta la baza de date pentru comunicare: " + fisVanzari + " " + ee.Message;
                    jurnal.scrie(eroare);
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
                if (!System.IO.File.Exists(fisVanzari)) { eroare = "Eroare: lipsa fisier: " + fisVanzari; jurnal.scrie(eroare); return; }
                //oCmd.CommandText = @"SELECT * FROM " + fisVanzari;
            }
            public DataTable getSales(DateTime initialPeriod, DateTime finalPeriod)
            {
                String initialDateTimeFile = "fa" + initialPeriod.Month.ToString() + initialPeriod.Year.ToString();
                String command = "SELECT fa.nrdoc, fa.data, fa.fuben, fb.denfb," +
                                        " fa.gest, fa.agent, fp.codp, fa.cant, fp.denp, fa.tvac," +
                                        " fa.datasc, fa.agent, agenti.nume, fa.pcv, fa.pv, fa.cant*fa.pv AS valpv " +
                                        "FROM " + initialDateTimeFile + " AS fa " +
                                        "LEFT JOIN fb on fa.fuben == fb.fuben " +
                                        "LEFT JOIN fp ON fp.codp == fa.codp " +
                                        "LEFT JOIN agenti ON agenti.agent == fa.agent";
                initialPeriod = initialPeriod.AddMonths(1);
                while (initialPeriod <= finalPeriod)
                {
                    initialDateTimeFile = "fa" + initialPeriod.Month.ToString() + initialPeriod.Year.ToString();
                    command = command + " UNION " +
                        "SELECT fa.nrdoc, fa.data, fa.fuben, fb.denfb," +
                                    " fa.gest, fa.agent, fp.codp, fa.cant, fp.denp, fa.tvac," +
                                    " fa.datasc, fa.agent, agenti.nume, fa.pcv, fa.pv, fa.cant*fa.pv AS valpv " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN fb on fa.fuben == fb.fuben " +
                                    "LEFT JOIN fp ON fp.codp == fa.codp " +
                                    "LEFT JOIN agenti ON agenti.agent == fa.agent";
                }
                oCmd.CommandText = command;
                DataTable dt = new DataTable();
                dt.Load(oCmd.ExecuteReader());
                dt.TableName = "fact";
                return dt;
            }
        }
    }
    public class Decryptor
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
}