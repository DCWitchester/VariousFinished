using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace POSTable
{
    /// <summary>
    /// the Global Object Structures for use within the Web Site
    /// </summary>
    public class ObjectStructures
    {
        /// <summary>
        /// the Menu Structure used for displaying a message
        /// </summary>
        public class Menu
        {
            /// <summary>
            /// the default Sale Client
            /// </summary>
            public String SaleClient = String.Concat(Enumerable.Repeat("X", 10));
            /// <summary>
            /// the used Table
            /// </summary>
            public Int32 Table { get; set; } 
            /// <summary>
            /// the initial SaleTime
            /// </summary>
            public DateTime SaleTime { get; set; }
            /// <summary>
            /// the itemList
            /// </summary>
            public List<MenuItem> itemList = new List<MenuItem>();
            /// <summary>
            /// the possible categories for the menu
            /// </summary>
            public List<MenuCategory> menuCategories = new List<MenuCategory>();

            /// <summary>
            /// the Main Function for Initializing the Menu from the WebService XML
            /// </summary>
            /// <param name="menu"></param>
            public void InitializeMenuFromServer(XmlClasses.Meniu menu)
            {
                //we iterate the elements in the XmlDeserialized Object
                foreach(var element in menu.Menu)
                {
                    //and add a new item to the list
                    itemList.Add(
                        new MenuItem 
                        { 
                            ProductCode = element.ProductCode, 
                            ProductName = element.ProductName,
                            ProductPrice = element.ProductPrice,
                            ProductCategory = element.ProductCategory,
                            DisplayOrder = element.DisplayOrder
                        });
                }
            }
            public void InitializeMenuCategoriesFromServer(XmlClasses.Categories categories, XmlClasses.Administrations administrations)
            {
                foreach(var element in categories.categories) 
                {
                    menuCategories.Add(new MenuCategory 
                    {
                        CategoryCode = element.CategoryCode,
                        CategoryName = element.CategoryName,
                        CategoryOrder = element.DisplayOrder,
                        AdministrationOrder = administrations.administrations
                                                    .Where(x=>x.AdminitrationCode==element.CategoryAdministration)
                                                    .FirstOrDefault()?.DisplayOrder
                    });
                }
            }

            /// <summary>
            /// this function creates an JSON String from the Sale
            /// </summary>
            /// <returns>the JSON Object</returns>
            public String CreateSale()
            {
                //we initialize a new XmlSale
                XmlClasses.Sale sale = new XmlClasses.Sale();
                //then foreach element in the current object
                foreach(var element in itemList)
                {
                    //we add a new item to the sale item
                    sale.Vanzare.Add(new XmlClasses.SaleItem
                    {
                        ProductCode = element.ProductCode,
                        ProductName = element.ProductName,
                        ProductPrice = element.ProductPrice,
                        ProductQuantity = element.ProductQuantity,
                        Table = Table,
                        SaleClient = SaleClient
                        //SaleTime = SaleTime
                    });
                }
                //then we serialize the object to the json
                String json = JsonConvert.SerializeObject(sale);
                return json;
            }

            /// <summary>
            /// this function will set a new sale time
            /// </summary>
            public void SetSaleTime()
            {
                SaleTime = DateTime.Now;
            }
        }

        /// <summary>
        /// the main MenuItem Object used for Display
        /// </summary>
        public class MenuItem
        {
            /// <summary>
            /// the Product Code
            /// </summary>
            public Int32 ProductCode { get; set; } = new Int32();
            /// <summary>
            /// the Product Name
            /// </summary>
            public String ProductName { get; set; } = String.Empty;
            /// <summary>
            /// the Product Price
            /// </summary>
            public Double ProductPrice { get; set; } = new Double();
            /// <summary>
            /// the Product Quantity
            /// </summary>
            public Int32 ProductQuantity { get; set; } = 0;
            /// <summary>
            /// the property used for Display Settings
            /// </summary>
            public Boolean IsSelected { get; set; } = false;
            /// <summary>
            /// the property used for the display order
            /// </summary>
            public Int32 DisplayOrder { get; set; } = 0;
            public Int32 ProductCategory { get; set; } = 0;
        }

        /// <summary>
        /// the MenuCategory for filtered Display
        /// </summary>
        public class MenuCategory 
        {
            public Int32 CategoryCode { get; set; } = 0;
            public String CategoryName { get; set; } = String.Empty;
            public Int32 CategoryOrder { get; set; } = 0;
            public Int32? AdministrationOrder { get; set; } = 0;
            public Boolean IsOpened { get; set; } = false;

        }
    }
}
