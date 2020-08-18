using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POSTable
{
    /// <summary>
    /// the main XmlClasses for containing the Object for Serialization and Deserialization
    /// </summary>
    public class XmlClasses
    {
        /// <summary>
        /// the MenuItem Object 
        /// </summary>
        public class MenuItem
        {
            [XmlAttribute]
            public Int32 ProductCode { get; set; }
            [XmlAttribute]
            public String ProductName { get; set; }
            [XmlAttribute]
            public Double ProductPrice { get; set; }
            [XmlAttribute]
            public Int32 ProductCategory { get; set; }
            [XmlAttribute]
            public Int32 DisplayOrder { get; set; }
        }

        /// <summary>
        /// the Meniu Root Element
        /// </summary>
        [XmlRoot(ElementName = "Meniu")]
        public class Meniu
        {
            public List<MenuItem> Menu { get; set; } = new List<MenuItem>();
        }

        /// <summary>
        /// the Answer XML Element
        /// </summary>
        [XmlRoot(ElementName = "Raspuns")]
        public class Answer
        {
            public Boolean Valoare { get; set; }
        }

        /// <summary>
        /// the SaleItem Object
        /// </summary>
        public class SaleItem
        {
            [XmlAttribute]
            public Int32 ProductCode { get; set; }
            [XmlAttribute]
            public String ProductName { get; set; }
            [XmlAttribute]
            public Double ProductPrice { get; set; }
            [XmlAttribute]
            public Int32 ProductQuantity { get; set; }
            /*
            [XmlAttribute]
            public DateTime SaleTime { get; set; }
            */
            [XmlAttribute]
            public String SaleClient { get; set; }
            [XmlAttribute]
            public Int32 Table { get; set; }
        }

        /// <summary>
        /// The Sale Object
        /// *Deprecated*
        /// </summary>
        [XmlRoot(ElementName = "Vanzare")]
        public class Sale
        {
            public List<SaleItem> Vanzare = new List<SaleItem>();
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
        }
    }
}
