using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    public class Table
    {
        [XmlAttribute]
        public Int32 TableID { get; set; }

        [XmlAttribute]
        public Int32 Status { get; set; }

        [XmlAttribute]
        public Int32 WaiterID { get; set; }

        [XmlAttribute]
        public String WaiterCode { get; set; }

        [XmlAttribute]
        public String WaiterName { get; set; }
    }
}