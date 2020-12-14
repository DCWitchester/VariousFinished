using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScanner.ObjectClasses
{
    public class Products
    {
#pragma warning disable IDE1006 // Naming Styles
        protected Int32 id { get; set; } = new Int32();

        protected String productCode { get; set; } = String.Empty;

        protected String productName { get; set; } = String.Empty;

        protected String productQuantity { get; set; } = String.Empty;
#pragma warning restore IDE1006 // Naming Styles

        public Int32 ID
        {
            get => id;
            set => id = value;
        }

        public String ProductCode 
        { 
            get => productCode; 
            set => productCode = value; 
        }

        public String ProductName 
        { 
            get => productName; 
            set => productName = value; 
        }

        public String ProductQuantity
        {
            get => productQuantity;
            set => productQuantity = value;
        }
    }
}
