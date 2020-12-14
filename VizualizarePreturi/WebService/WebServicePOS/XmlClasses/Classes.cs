using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServiceBergenbier
{
    public class Classes
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


            /// <summary>
            /// this function will initialize the object with values from the DataTable retrieved from Px
            /// </summary>
            /// <param name="dt">the dataTable</param>
            public void GetProductDisplayFromDataTable(DataTable dt)
            {
                // Errors might occur for lack of a unique key
                // Also empty keys produce errors
                ProductName = dt.Rows[0][0].ToString().Trim();
                ProductPrice = ((Decimal)dt.Rows[0][1]).ToString("0.00").Trim();
            }
        }

        public class Qunatities
        {
            public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();
        }

        public class ProductQuantity
        {
            /// <summary>
            /// The Product Code
            /// </summary>
            public String ProductCode { get; set; }
            /// <summary>
            /// The Product Name
            /// </summary>
            public String ProductName { get; set; }
            /// <summary>
            /// The Product Price
            /// </summary>
            public Double ProductPrice { get; set; }
            /// <summary>
            /// the Selected Quantity
            /// </summary>
            public Double ProductQunatity { get; set; }
        }
    }
}