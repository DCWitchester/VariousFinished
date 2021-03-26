using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the main Menu Item for xmlSerialization
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// the Product Code
        /// </summary>
        [XmlAttribute]
        public Int32 ProductCode { get; set; }
        /// <summary>
        /// the Product Name
        /// </summary>
        [XmlAttribute]
        public String ProductName { get; set; }
        /// <summary>
        /// the Product Price
        /// </summary>
        [XmlAttribute]
        public Double ProductPrice { get; set; }
        /// <summary>
        /// the product category used for filtering
        /// </summary>
        [XmlAttribute]
        public Int32 ProductCategory { get; set; }
        /// <summary>
        /// the Display Order for the products
        /// </summary>
        [XmlAttribute]
        public Int32 DisplayOrder { get; set; }
    }
}