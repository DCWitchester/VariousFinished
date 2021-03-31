using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WebServicePOS.Classes;

namespace WebServicePOS.VFPClasses.POSTable
{
    public class PosTableWaiter : VFPConnection
    {
        public XmlDocument GetWaiter(String waiterCode)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            String command = $"SELECT RECNO() as _id,ccod,cnume FROM {base.fisPers} WHERE ccod = PADL('{waiterCode}',10,'0')";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            try
            {
                base.OpenConnection();
                dt.Load(oCmd.ExecuteReader());
                base.CloseConnection();
            }
            catch { }
            dt.TableName = "Ospatar";
            Classes.Waiter waiter = new Classes.Waiter();
            waiter.SetWaiterFromDataTable(dt);
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.Waiter));
            String result = String.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, waiter);
                memoryStream.Position = 0;
                result = new StreamReader(memoryStream).ReadToEnd();
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);
            return xmlDocument;
        }

        public XmlDocument CheckWaiter(String waiterCode)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            String command = $"SELECT COUNT(*) as number FROM {base.fisPers} WHERE ccod = PADL('{waiterCode}',10,'0')";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            base.OpenConnection();
            //initialize a new tableCount
            //try to parse the retrieved Value into the tableCount
            if (!Int32.TryParse(oCmd.ExecuteScalar().ToString(), out Int32 tableCount))
            {
                //if we fail we initialize the count to 0
                tableCount = 0;
            }
            base.CloseConnection();
            //we initialize a new Answer Object
            Answer answer = new Answer
            {
                //we set the answer value if the table count is greater than 0 or not
                //ea if the table is open or not
                Valoare = tableCount > 0
            };
            //we initialize a new serializer over the object
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.Answer));
            //and a new string for the memory strean
            String result = String.Empty;
            //and using a new memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //serialize the object to the stream
                serializer.Serialize(memoryStream, answer);
                //position the cursor at the start of the stream
                memoryStream.Position = 0;
                //then dump the whole stream into the string
                result = new StreamReader(memoryStream).ReadToEnd();
            }
            //intialize the xmlDocument 
            XmlDocument xmlDocument = new XmlDocument();
            //load the string into the xml
            xmlDocument.LoadXml(result);
            //and return the xmlDocument
            return xmlDocument;
        }

        public XmlDocument GetTables()
        {
            System.Data.OleDb.OleDbCommand oCmd = FileBaseConnection.CreateCommand();
            String command = $@"SELECT mq.masa, NVL(vanz.stare,0) AS stare, NVL(pr._id,-1) AS _id,
                                    NVL(pr.ccod,SPACE(10)) as ccod, NVL(pr.cnume,SPACE(35)) as cnume 
                                FROM (SELECT RECNO() as masa FROM {base.fisMese}) AS mq
                                LEFT JOIN ( SELECT vanz.ntable , MAX(vanz.stare) as stare, vanz.cuser FROM {base.fisVanzari} GROUP BY vanz.ntable) as vanz
                                    ON vanz.ntable == mq.masa
                                LEFT JOIN (SELECT RECNO() as _id, ccod, cnume FROM {base.fisPers}) AS pr
                                    ON pr.ccod == vanz.cuser";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            try
            {
                base.OpenConnection();
                dt.Load(oCmd.ExecuteReader());
                base.CloseConnection();
            }catch{ }
            dt.TableName = "Mese";
            Classes.Tables tables = new Classes.Tables();
            tables.SetListFromDataTable(dt);
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.Tables));
            String result = String.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, tables);
                memoryStream.Position = 0;
                result = new StreamReader(memoryStream).ReadToEnd();
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);
            return xmlDocument;
        }

        /// <summary>
        /// this function will return the active sale for a given table
        /// </summary>
        /// <param name="table">the given table</param>
        /// <returns></returns>
        public XmlDocument GetSaleOfTable(String table)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            //the new Select Command
            String command = String.Format("SELECT SUM(ncant) as ncant,ncodp FROM {0} GROUP BY ncodp,ntable WHERE ntable = {1}", fisVanzari, table);
            //and set the command to the OleDb
            oCmd.CommandText = command;
            //initialize a new DataTable
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            base.CloseConnection();
            List<OpenSale> openSales = dt.AsEnumerable().Select(element => new OpenSale
            {
                ProductCode = (Int32)element.Field<Decimal>("ncodp"),
                ProductQuantity = (Int32)element.Field<Decimal>("ncant")
            }).ToList();
            //we initialize a new serializer over the object
            XmlSerializer serializer = new XmlSerializer(typeof(List<OpenSale>));
            //and a new string for the memory strean
            String result = String.Empty;
            //and using a new memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //serialize the object to the stream
                serializer.Serialize(memoryStream, openSales);
                //position the cursor at the start of the stream
                memoryStream.Position = 0;
                //then dump the whole stream into the string
                result = new StreamReader(memoryStream).ReadToEnd();
            }
            //intialize the xmlDocument 
            XmlDocument xmlDocument = new XmlDocument();
            //load the string into the xml
            xmlDocument.LoadXml(result);
            //and return the xmlDocument
            return xmlDocument;
        }
    }
}