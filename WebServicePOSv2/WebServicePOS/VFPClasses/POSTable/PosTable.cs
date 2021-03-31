using Newtonsoft.Json;
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
    public class PosTable : VFPConnection
    {
        #region TableMenu

        #region Data Collection
        /// <summary>
        /// the Main Function for the Menu Item
        /// </summary>
        /// <returns>an XmlDocument Containing the Menu</returns>
        public XmlDocument GetMenu()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            //we create a new command select string
            String command = String.Format(@"SELECT ncod,cden,npv,ncateg,nord 
                                                FROM {0} 
                                                WHERE lactiv = .t.", fisProd);
            //set it to the command
            oCmd.CommandText = command;
            //initialize a new DataTable
            DataTable dt = new DataTable();
            //and load the datatable from the reader
            try
            {
                base.OpenConnection();
                dt.Load(oCmd.ExecuteReader());
                base.CloseConnection();
            }
            catch { }
            //set a name for the dataTable
            //Why? => Because I can
            dt.TableName = "Meniu";
            //initialize a new classes object
            Classes.Meniu meniu = new Classes.Meniu();
            //and dump the data from the DataTable ibto it
            meniu.SetMenuFromDataTable(dt);
            //create a new serializer over the object
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.Meniu));
            //initialize a new String
            String result = String.Empty;
            //and using the memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //serialize the object to the stream
                serializer.Serialize(memoryStream, meniu);
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

        public XmlDocument GetAdministrations()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            String command = String.Format(@"SELECT cgest,cdeng,nord FROM {0} ORDER BY nord", fisGest);
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            try
            {
                base.OpenConnection();
                dt.Load(oCmd.ExecuteReader());
                base.CloseConnection();
            }
            catch { }
            dt.TableName = "Gestiuni";
            Classes.Administrations administrations = new Classes.Administrations();
            administrations.SetAdministrationsFromDataTable(dt);
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.Administrations));
            //initialize a new String
            String result = String.Empty;
            //and using the memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //serialize the object to the stream
                serializer.Serialize(memoryStream, administrations);
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

        public XmlDocument GetCategories()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            String command = String.Format("SELECT ncod,cgest,cden,nord FROM {0} ORDER BY nord", fisCateg);
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            try
            {
                base.OpenConnection();
                dt.Load(oCmd.ExecuteReader());
                base.CloseConnection();
            }
            catch { }
            dt.TableName = "Categorii";
            Classes.Categories categories = new Classes.Categories();
            categories.setCategoriesFromDataTable(dt);
            XmlSerializer serializer = new XmlSerializer(typeof(Classes.Categories));
            //initialize a new String
            String result = String.Empty;
            //and using the memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //serialize the object to the stream
                serializer.Serialize(memoryStream, categories);
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

        #endregion

        #region Sale
        /// <summary>
        /// this function will insert a new sale received as a JSON Object
        /// </summary>
        /// <param name="saleDocument">the new sale Document</param>
        public void SetSale(String saleDocument)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            //we Deserialize the Object into the sale
            Classes.Sale sale = JsonConvert.DeserializeObject<Classes.Sale>(saleDocument);
            //we create a new command to retrieve the new sale id from the program settings
            String command = @"SELECT TOP 1 nbon FROM setari ORDER BY nbon";
            //set the command for the OleDB 
            oCmd.CommandText = command;
            //initialize a new sale ID
            //and try to parse the element retrieved from the database into the newSaleID
            if (!Int32.TryParse(oCmd.ExecuteScalar().ToString(), out Int32 newSaleID))
            {
                //if we are unable we initialize the ID to 0
                newSaleID = 0;
            }
            //else we will increment the new SaleID
            else newSaleID++;
            //and create a new command to update the value in the settings
            command = @"UPDATE setari SET nbon = nbon + 1";
            //set the command to the OleDB
            oCmd.CommandText = command;
            //and execute the update
            oCmd.ExecuteNonQuery();
            //and create a new command to retrieve the unique sale id 
            command = @"SELECT TOP 1 vanz_uid FROM lastcod ORDER BY vanz_uid";
            //set the command to the OleDB
            oCmd.CommandText = command;
            //initialize a newItemID
            //and try to parse the element retrieved from the database
            if (!Int32.TryParse(oCmd.ExecuteScalar().ToString(), out Int32 newItemID))
            {
                //if I fail I initialize the id to 0
                newItemID = 0;
            }
            //else we increment the id with 1
            else newItemID++;
            //and initialize a new update string for initialize the new Sale Item ID in the Settings
            command = String.Format(@"UPDATE lastcod SET vanz_uid = vanz_uid + {0}", sale.Vanzare.Count());
            //set the command to the OleDB
            oCmd.CommandText = command;
            //and execute the query
            oCmd.ExecuteNonQuery();
            //we also initialize a new List over the DBFSale
            List<Classes.DBFSale> dBFSales = new List<Classes.DBFSale>();
            //and create the dbfSale from the current sale and the id Items
            sale.SetDbfSaleFromSale(dBFSales, newSaleID, newItemID);
            //then foreach element in the list
            foreach (var element in dBFSales)
            {
                //we create a new insert command
                command = String.Format(@"INSERT INTO vanz(tora,cuser,ntable,nbon,ncodp,cden,npret,ncant,nid,uid,nota,stare)
                                                VALUES(DateTime(),'{1}',{2},{3},{4},'{5}',{6},{7},{8},{9},{10},4)",
                                                element.SaleTime,
                                                element.SaleClient,
                                                element.Table,
                                                element.SaleNumber,
                                                element.ProductCode,
                                                element.ProductName,
                                                element.ProductPrice,
                                                element.ProductQuantity,
                                                element.SaleID,
                                                element.ItemID,
                                                1);
                //add it to the OleDB
                oCmd.CommandText = command;
                //and Execute the Query
                oCmd.ExecuteNonQuery();
                
            };
            base.CloseConnection();
        }
        #endregion

        /// <summary>
        /// this function checks if a table is already open
        /// </summary>
        /// <param name="table">the Table ID</param>
        /// <returns>the XmlDocument</returns>
        public XmlDocument GetIsTableOpen(String table)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            //the new Select Command
            String command = String.Format("SELECT COUNT(*) FROM {0} WHERE ntable = {1}", fisVanzari, table);
            //and set the command to the OleDb
            oCmd.CommandText = command;
            //initialize a new tableCount
            //try to parse the retrieved Value into the tableCount
            if (!Int32.TryParse(oCmd.ExecuteScalar().ToString(), out Int32 tableCount))
            {
                //if we fail we initialize the count to 0
                tableCount = 0;
            }
            base.CloseConnection();
            //we initialize a new Answer Object
            Classes.Answer answer = new Classes.Answer
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


        #endregion
    }
}