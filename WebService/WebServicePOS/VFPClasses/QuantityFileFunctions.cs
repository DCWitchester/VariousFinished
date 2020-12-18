using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WebServiceEvidenta.VFPClasses
{
    public class QuantityFileFunctions: VFPConnection 
    {
        #region Public Functionality
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
                                                base.ProductGlossary,
                                                productCode.ToUpper().Trim());
            //we initialize a new command
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            //then set the command text for the ole object
            oCmd.CommandText = command;
            //then initialize a new dataTable
            DataTable dt = new DataTable();
            //we try to open the connection on the base class
            if (!base.OpenConnection()) return null;
            try
            {
                //and load it from the reader
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally
            {
                //always remember the fifth of november
                //oh, and to close your connection
                base.CloseConnection();
            }
            //and set a name for the table just because I can
            dt.TableName = "Produs";
            //we initialize a new productDisplay  
            SerializationClasses.ProductDisplay productDisplay = new SerializationClasses.ProductDisplay();
            //then we will retrieve the data from the table and fill the object
            productDisplay.GetProductDisplayFromDataTable(dt);
            //we initialize a serializer over tge ProductDisplay classes
            XmlSerializer serializer = new XmlSerializer(typeof(SerializationClasses.ProductDisplay));
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
        /// this function will dump a json file to the
        /// </summary>
        /// <param name="quantityDocument">the json document</param>
        public void SetQuantityFile(String quantityDocument)
        {
            //we Deserialize the Object into the sale
            SerializationClasses.Quantities quantities = JsonConvert.DeserializeObject<SerializationClasses.Quantities>(quantityDocument);
            CheckQuantityFile();
            //we initialize a new command
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            //we try to open the connection on the base class
            if (!base.OpenConnection()) return;
            //then we iterate the list elements
            foreach (var element in quantities.ProductQuantities)
            {
                String command = String.Format("INSERT INTO '{0}' " +
                                                        "VALUES ('{1}',{2}) " + Environment.NewLine,
                                                        QuantityFile,
                                                        element.ProductCode.Substring(0, 12),
                                                        element.ProductQunatity
                                                        );
                oCmd.CommandText = command;
                oCmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region LocalFunctionality
        /// <summary>
        /// this function will check the existance of the Quantity file and create it if needed
        /// </summary>
        void CheckQuantityFile()
        {
            if (!File.Exists(base.QuantityFile))
            {
                //the base string command for the creation of the file
                String commad = $"CREATE TABLE '{base.QuantityFile}' {base.QuantityFileStructure})";
                //we initialize a new command
                System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
                //set the command text to the db command
                oCmd.CommandText = commad;
                //we try to open the connection on the base class
                if (!base.OpenConnection()) return;
                try
                {
                    //and load it from the reader
                    oCmd.ExecuteNonQuery();
                }
                catch { return; }
                finally
                {
                    //always remember the fifth of november
                    //oh, and to close your connection
                    base.CloseConnection();
                }
            }
        }
        #endregion
    }
}