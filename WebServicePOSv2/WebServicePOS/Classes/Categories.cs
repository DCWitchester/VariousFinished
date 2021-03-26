using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the Root item for the XML Categories
    /// </summary>
    [XmlRoot(ElementName = "Gategorii")]
    public class Categories
    {
        public List<Category> categories { get; set; } = new List<Category>();

        public void setCategoriesFromDataTable(DataTable dt)
        {
            foreach (DataRow element in dt.Rows)
            {
                categories.Add(new Category
                {
                    CategoryCode = (Int32)((Decimal)element[0]),
                    CategoryAdministration = element[1].ToString(),
                    CategoryName = element[2].ToString(),
                    DisplayOrder = (Int32)((Decimal)element[3])
                });
            }
        }
    }
}