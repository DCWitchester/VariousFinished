using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceEvidenta.SerializationClasses
{
    public class Quantities
    {
        /// <summary>
        /// the main list of productQuantities for serialization/deserialization
        /// </summary>
        public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();
    }
}