using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the XmlRoot for the waiter item
    /// </summary>
    [XmlRoot(ElementName = "Ospatar")]
    public class Waiter
    {
        [XmlAttribute]
        public Int32 ID { get; set; }

        [XmlAttribute]
        public String WaiterCode { get; set; }

        [XmlAttribute]
        public String WaiterName { get; set; }

        /// <summary>
        /// this function will dump the DataTable into this object
        /// </summary>
        /// <param name="dt">the given DataTable</param>
        public void SetWaiterFromDataTable(DataTable dt)
        {
            this.ID = (Int32)((Decimal)dt.Rows[0][0]);
            this.WaiterCode = dt.Rows[0][1].ToString();
            this.WaiterName = dt.Rows[0][2].ToString();
        }
    }
}