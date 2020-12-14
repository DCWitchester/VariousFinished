using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScanner.ObjectClasses
{
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
