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
        [XmlRoot(ElementName = "Produs")]
        public class ProductDisplay
        {
            public String ProductName { get; set; }
            public String ProductPrice { get; set; }

            public void GetProductDisplayFromDataTable(DataTable dt)
            {
                ProductName = dt.Rows[0][0].ToString().Trim();
                ProductPrice = ((Decimal)dt.Rows[0][1]).ToString("0.00").Trim();
            }
        }
    }
}