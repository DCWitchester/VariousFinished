using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the Root Element dor the XmlSerialization
    /// </summary>
    [XmlRoot(ElementName = "Meniu")]
    public class Meniu
    {
        /// <summary>
        /// the complete Item List 
        /// </summary>
        public List<MenuItem> Menu { get; set; } = new List<MenuItem>();
        /// <summary>
        /// this function will set the Menu Object from the DataTable retrieve from the Fox Tables
        /// </summary>
        /// <param name="dt">the Fox DataTable</param>
        public void SetMenuFromDataTable(DataTable dt)
        {
            //we iterate the element Rows
            foreach (DataRow element in dt.Rows)
            {
                //and add to the object list a new MenuItem
                Menu.Add(new MenuItem
                {
                    ProductCode = (Int32)((Decimal)element[0]),
                    ProductName = element[1].ToString().Trim(),
                    ProductPrice = (Double)((Decimal)element[2]),
                    ProductCategory = (Int32)((Decimal)element[3]),
                    DisplayOrder = (Int32)((Decimal)element[4])
                });
            }
        }
    }
}