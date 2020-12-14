using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BarcodeScanner
{
    class XmlClasses
    {
        /// <summary>
        /// the root element for the ProductDisplay XML Object
        /// </summary>
        [XmlRoot(ElementName = "Produs")]
        public class ProductDisplay
        {
            //The Product Name and Price are not [XmlAtrributes] because it caused errors on deserialization.
            /// <summary>
            /// The Product Name
            /// </summary>
            public String ProductName { get; set; }
            /// <summary>
            /// The Product Price
            /// </summary>
            public String ProductPrice { get; set; }
        }
    }
}
