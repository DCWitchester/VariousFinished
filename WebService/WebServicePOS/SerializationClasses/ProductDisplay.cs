using System;
using System.Data;
using System.Xml.Serialization;

namespace WebServiceEvidenta.SerializationClasses
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
}