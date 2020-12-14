using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScanner.ObjectClasses
{
    public class Qunatities
    {
        public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

        public String SetQuantitiesFromProductList(List<Products> products)
        {
            foreach(var element in products)
            {
                ProductQuantities.Add(new ProductQuantity
                {
                    ProductCode = element.ProductCode,
                    ProductName = element.ProductName,
                    ProductPrice = 0,
                    ProductQunatity = Convert.ToDouble(element.ProductQuantity)

                });
            }
            String json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
