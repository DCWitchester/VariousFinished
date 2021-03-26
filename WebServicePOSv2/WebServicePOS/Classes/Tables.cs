using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    [XmlRoot(ElementName = "Mese")]
    public class Tables
    {
        public List<Table> tables { get; set; } = new List<Table>();

        /// <summary>
        /// this function will dump the dataTable to a list of tables
        /// </summary>
        /// <param name="dt">the given DataTable</param>
        public void SetListFromDataTable(DataTable dt)
        {
            tables = dt.AsEnumerable().Select(element => new Table 
            { 
                TableID = (Int32)element.Field<Decimal>("masa"),
                Status = (Int32)element.Field<Decimal>("stare"),
                WaiterID = (Int32)element.Field<Decimal>("_id"),
                WaiterCode = element.Field<String>("ccod").Trim(),
                WaiterName = element.Field<String>("cnume").Trim()
            }).ToList();
        }
    }
}