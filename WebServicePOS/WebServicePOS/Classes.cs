using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS
{
    /// <summary>
    /// the main class for containing all object classes
    /// </summary>
    public class Classes
    {
        /// <summary>
        /// the main Menu Item for xmlSerialization
        /// </summary>
        public class MenuItem
        {
            /// <summary>
            /// the Product Code
            /// </summary>
            [XmlAttribute]
            public Int32 ProductCode { get; set; }
            /// <summary>
            /// the Product Name
            /// </summary>
            [XmlAttribute]
            public String ProductName { get; set; }
            /// <summary>
            /// the Product Price
            /// </summary>
            [XmlAttribute]
            public Double ProductPrice { get; set; }
            /// <summary>
            /// the product category used for filtering
            /// </summary>
            [XmlAttribute]
            public Int32 ProductCategory { get; set; }
            /// <summary>
            /// the Display Order for the products
            /// </summary>
            [XmlAttribute]
            public Int32 DisplayOrder { get; set; }
        }

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
                foreach(DataRow element in dt.Rows)
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

        /// <summary>
        /// the XmlRoot Element for Simple Boolean Answers
        /// </summary>
        [XmlRoot(ElementName = "Raspuns")]
        public class Answer
        {
            /// <summary>
            /// the Answer Value
            /// </summary>
            public Boolean Valoare { get; set; }
        }

        /// <summary>
        /// the main Administration Item for use with XML Serialization
        /// </summary>
        public class AdministrationItem
        {
            [XmlAttribute]
            public String AdminitrationCode { get; set; }
            [XmlAttribute]
            public String AdministrationName { get; set; }
            [XmlAttribute]
            public Int32 DisplayOrder { get; set; }
        }

        /// <summary>
        /// the XmlRoot for the Administrations List
        /// </summary>
        [XmlRoot(ElementName = "Gest")]
        public class Administrations
        {
            public List<AdministrationItem> administrations { get; set; } = new List<AdministrationItem>();

            public void SetAdministrationsFromDataTable(DataTable dt)
            {
                foreach(DataRow element in dt.Rows)
                {
                    administrations.Add(new AdministrationItem
                    {
                        AdminitrationCode = element[0].ToString(),
                        AdministrationName = element[1].ToString(),
                        DisplayOrder = (Int32)((Decimal)element[2])
                    });
                }
            }
        }

        /// <summary>
        /// the Category Item for the XML Serialization
        /// </summary>
        public class Category
        {
            [XmlAttribute]
            public Int32 CategoryCode { get; set; }
            [XmlAttribute]
            public String CategoryAdministration { get; set; }
            [XmlAttribute]
            public String CategoryName { get; set; }
            [XmlAttribute]
            public Int32 DisplayOrder { get; set; }
        }

        /// <summary>
        /// the Root item for the XML Categories
        /// </summary>
        [XmlRoot(ElementName = "Gategorii")]
        public class Categories
        {
            public List<Category> categories { get; set; } = new List<Category>();

            public void setCategoriesFromDataTable(DataTable dt)
            {
                foreach(DataRow element in dt.Rows)
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

        /// <summary>
        /// the Main Sale Item for the insert into the Fox DataTable
        /// </summary>
        public class SaleItem
        {
            /// <summary>
            /// the Product Code
            /// </summary>
            [XmlAttribute]
            public Int32 ProductCode { get; set; }
            /// <summary>
            /// the Product Name
            /// </summary>
            [XmlAttribute]
            public String ProductName { get; set; }
            /// <summary>
            /// the Product Price
            /// </summary>
            [XmlAttribute]
            public Double ProductPrice { get; set; }
            /// <summary>
            /// the Product Quantity
            /// </summary>
            [XmlAttribute]
            public Int32 ProductQuantity { get; set; }
            /*
            [XmlAttribute]
            public DateTime SaleTime { get; set; }
            */
            /// <summary>
            /// the Sale Client
            /// </summary>
            [XmlAttribute]
            public String SaleClient { get; set; }
            /// <summary>
            /// the Table
            /// </summary>
            [XmlAttribute]
            public Int32 Table { get; set; }
        }
        /// <summary>
        /// the main Sale  Xml Object
        /// </summary>
        [XmlRoot(ElementName = "Vanzare")]
        public class Sale
        {
            /// <summary>
            /// the Sale Item List
            /// </summary>
            public List<SaleItem> Vanzare = new List<SaleItem>();

            /// <summary>
            /// the main function for creating a DBF Sale from the current Sale Object
            /// </summary>
            /// <param name="dBFSales">the new DBF Sale list</param>
            /// <param name="saleID">the new ID for the Sale</param>
            /// <param name="itemID">the initial id for the item</param>
            public void SetDbfSaleFromSale(List<DBFSale> dBFSales,Int32 saleID,Int32 itemID)
            {
                //we iterate the current object Sale List
                foreach(var element in Vanzare)
                {
                    //then add a new item in the dbfSale list
                    dBFSales.Add(new DBFSale
                    {
                        ProductCode = element.ProductCode,
                        ProductName = element.ProductName,
                        ProductPrice = element.ProductPrice,
                        ProductQuantity = element.ProductQuantity,
                        SaleClient = element.SaleClient,
                        Table = element.Table,
                        SaleID = saleID,
                        SaleNumber = saleID,
                        ItemID = itemID
                    });
                    //then increment the id for the next sale item
                    itemID++;
                }
            }
        }

        /// <summary>
        /// the main DBF Sale Class
        /// </summary>
        public class DBFSale
        {
            /// <summary>
            /// the product code
            /// </summary>
            public Int32 ProductCode { get; set; } = new Int32();
            /// <summary>
            /// the product name
            /// </summary>
            public String ProductName { get; set; } = String.Empty;
            /// <summary>
            /// the product price
            /// </summary>
            public Double ProductPrice { get; set; } = new Double();
            /// <summary>
            /// the product quantity
            /// </summary>
            public Int32 ProductQuantity { get; set; } = new Int32();
            /// <summary>
            /// the sale time
            /// </summary>
            public DateTime SaleTime { get; set; } = new DateTime();
            /// <summary>
            /// the sale client
            /// </summary>
            public String SaleClient { get; set; } = String.Empty;
            /// <summary>
            /// the Table Number
            /// </summary>
            public Int32 Table { get; set; } = new Int32();
            /// <summary>
            /// the Sale Number
            /// </summary>
            public Int32 SaleNumber { get; set; } = new Int32();
            /// <summary>
            /// the Sale ID
            /// </summary>
            public Int32 SaleID { get; set; } = new Int32();
            /// <summary>
            /// the Item ID
            /// </summary>
            public Int32 ItemID { get; set; } = new Int32();
        }

    }
}