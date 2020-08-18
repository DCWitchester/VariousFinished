using ProductDisplay.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductDisplay.WebFunctions
{
    public class XmlObjects
    {
        [XmlRoot(ElementName = "Produs")]
        public class ProductDisplay
        {
            public String ProductName { get; set; }
            public String ProductPrice { get; set; }

            public void SetProductDisplayToObject(ProductController productController)
            {
                productController.ProductName = this.ProductName;
                productController.ProductPrice = this.ProductPrice;
            }
        }
    }
}
