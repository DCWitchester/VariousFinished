using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the Main Sale Item for the insert into the Fox DataTable
    /// </summary>
    public class SaleItem
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
        /// the Product Quantity
        /// </summary>
        [XmlAttribute]
        public Int32 ProductQuantity { get; set; }
        /*
        [XmlAttribute]
        public DateTime SaleTime { get; set; }
        */
        /// <summary>
        /// the Sale Client
        /// </summary>
        [XmlAttribute]
        public String SaleClient { get; set; }
        /// <summary>
        /// the Table
        /// </summary>
        [XmlAttribute]
        public Int32 Table { get; set; }
    }
}