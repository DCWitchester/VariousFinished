using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the Category Item for the XML Serialization
    /// </summary>
    public class Category
    {
        [XmlAttribute]
        public Int32 CategoryCode { get; set; }
        [XmlAttribute]
        public String CategoryAdministration { get; set; }
        [XmlAttribute]
        public String CategoryName { get; set; }
        [XmlAttribute]
        public Int32 DisplayOrder { get; set; }
    }
}