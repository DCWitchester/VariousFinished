using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WebServiceEvidenta.VFPClasses
{
    public class ProductDisplayFunctions : VFPConnection
    {

        /// <summary>
        /// this is the main function for retriving the name and the price of a given ProductCode
        /// </summary>
        /// <param name="productCode">the given product code</param>
        /// <returns>the xmlDocument containing the product name and price</returns>
        public XmlDocument getProductDetails(String productCode)
        {
            //we set the command string
            String command = String.Format("SELECT TOP 1 denm,pv FROM '{0}' WHERE UPPER(ALLTRIM(codp)) == '{1}' ORDER BY codp",
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
    }
}