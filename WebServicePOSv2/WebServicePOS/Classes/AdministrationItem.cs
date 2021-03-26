using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the main Administration Item for use with XML Serialization
    /// </summary>
    public class AdministrationItem
    {
        [XmlAttribute]
        public String AdminitrationCode { get; set; }
        [XmlAttribute]
        public String AdministrationName { get; set; }
        [XmlAttribute]
        public Int32 DisplayOrder { get; set; }
    }
}