using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
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
            //the static objects fot accessing file Paths
            static readonly String EvidentaPath = Miscellaneous.Decryptor.Decrypt(Properties.Settings.Default.CaleEvidenta);
            /// <summary>
            /// FoxUser is used for odbcValidity Check
            /// </summary>
            static readonly String odbcCheck = EvidentaPath + @"\FOXUSER.DBF";
            //the DBF and NOM Path
            static readonly String dbfPath = EvidentaPath + @"\DBF";
            static readonly String nomPath = EvidentaPath + @"\NOM";
            /// <summary>
            /// the oledb Connection
            /// </summary>
            System.Data.OleDb.OleDbConnection dbfConn = new System.Data.OleDb.OleDbConnection();
            /// <summary>
            /// the main Command 
            /// </summary>
            System.Data.OleDb.OleDbCommand oCmd;
            #region Nomenclator Files
            readonly string NomenclatorProduse = nomPath + @"\FP.DBF";
            #endregion
            #region Document Files
            readonly string QuantityFile = dbfPath + @"\CantitatiProduse.DBF";
            #endregion
            //the needed preset settings for fox.
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

            /// <summary>
            /// the main caller for the vfpPOS
            /// </summary>
            public vfpPOS()
            {
                #region initializare vfpoledb
                //we initialize the connectionString
                dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + EvidentaPath + ";Collating Sequence=general;";
                //try to open the dbfConnection
                try
                {
                    dbfConn.Open();
                }catch (Exception e)
                {
                    return;
                }
                //then we set the needed presets
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
          
            /// <summary>
            /// this is the main function for retriving the name and the price of a given ProductCode
            /// </summary>
            /// <param name="productCode">the given product code</param>
            /// <returns>the xmlDocument containing the product name and price</returns>
            public XmlDocument getProductDetails(String productCode)
            { 
                //we set the command string
                String command = String.Format("SELECT TOP 1 denm,pv FROM '{0}' WHERE UPPER(ALLTRIM(codp)) == '{1}' ORDER BY codp",
                                                    NomenclatorProduse,
                                                    productCode.ToUpper().Trim());
                //then set the command text for the ole object
                oCmd.CommandText = command;
                //then initialize a new dataTable
                DataTable dt = new DataTable();
                try
                {
                    //and load it from the reader
                    dt.Load(oCmd.ExecuteReader());
                } catch { }
                //and set a name for the table just because I can
                dt.TableName = "Produs";
                //we initialize a new productDisplay  
                Classes.ProductDisplay productDisplay = new Classes.ProductDisplay();
                //then we will retrieve the data from the table and fill the object
                productDisplay.GetProductDisplayFromDataTable(dt);
                //we initialize a serializer over tge ProductDisplay classes
                XmlSerializer serializer = new XmlSerializer(typeof(Classes.ProductDisplay));
                //we initialize a resultString
                String result = String.Empty;
                //then using a memoryStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //we serialize the object onto the memoryStream
                    serializer.Serialize(memoryStream, productDisplay);
                    //then place myself at the start of the stream
                    memoryStream.Position = 0;
                    //and dump the string into the result string
                    result = new StreamReader(memoryStream).ReadToEnd();
                }
                //we create a new XmlDocument
                XmlDocument xmlDocument = new XmlDocument();
                //and load it from the resulted string
                xmlDocument.LoadXml(result);
                //before finally returning the result.
                return xmlDocument;
            }

            /// <summary>
            /// this is the main function for retriving the name and the price of a given ProductCode
            /// </summary>
            /// <param name="productCode">the given product code</param>
            /// <returns>the xmlDocument containing the product name and price</returns>
            public XmlDocument getProductName(String productCode)
            {
                //we set the command string
                String command = String.Format("SELECT TOP 1 denm,pv " +
                                                    "FROM '{0}' " +
                                                    "WHERE UPPER(ALLTRIM(codp)) == '{1}' OR UPPER(ALLTRIM(codext)) == '{1}' " +
                                                    "ORDER BY codp",
                                                    NomenclatorProduse,
                                                    productCode.ToUpper().Trim());
                //then set the command text for the ole object
                oCmd.CommandText = command;
                //then initialize a new dataTable
                DataTable dt = new DataTable();
                try
                {
                    //and load it from the reader
                    dt.Load(oCmd.ExecuteReader());
                }
                catch { }
                //and set a name for the table just because I can
                dt.TableName = "Produs";
                //we initialize a new productDisplay  
                Classes.ProductDisplay productDisplay = new Classes.ProductDisplay();
                //then we will retrieve the data from the table and fill the object
                productDisplay.GetProductDisplayFromDataTable(dt);
                //we initialize a serializer over tge ProductDisplay classes
                XmlSerializer serializer = new XmlSerializer(typeof(Classes.ProductDisplay));
                //we initialize a resultString
                String result = String.Empty;
                //then using a memoryStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //we serialize the object onto the memoryStream
                    serializer.Serialize(memoryStream, productDisplay);
                    //then place myself at the start of the stream
                    memoryStream.Position = 0;
                    //and dump the string into the result string
                    result = new StreamReader(memoryStream).ReadToEnd();
                }
                //we create a new XmlDocument
                XmlDocument xmlDocument = new XmlDocument();
                //and load it from the resulted string
                xmlDocument.LoadXml(result);
                //before finally returning the result.
                return xmlDocument;
            }

            public void SetQuantityFile(String qunatityDocument)
            {
                //we Deserialize the Object into the sale
                Classes.Qunatities quantities = JsonConvert.DeserializeObject<Classes.Qunatities>(qunatityDocument);
                CheckQuantityFile();
                //we set the command string
                StringBuilder builder = new StringBuilder(50 * quantities.ProductQuantities.Count);

                foreach (var element in quantities.ProductQuantities) 
                {
                    builder.Append(String.Format("INSERT INTO '{0}' " +
                                                        "VALUES ('{1}',{2}); ",
                                                        QuantityFile,
                                                        element.ProductCode.Substring(0, 12),
                                                        element.ProductQunatity
                                                        ));
                }

                oCmd.CommandText = builder.ToString();
                oCmd.ExecuteNonQuery();
            }
            #endregion

            #region Functionality
            void CheckQuantityFile()
            {
                if (!File.Exists(QuantityFile))
                {
                    String command = String.Format("CREATE TABLE '{0}' (code Character(12), quantity Numeric(18,4))", QuantityFile);
                    //then set the command text for the ole object
                    oCmd.CommandText = command;
                    oCmd.ExecuteNonQuery();
                }
            }
            #endregion 
        }
    }
}