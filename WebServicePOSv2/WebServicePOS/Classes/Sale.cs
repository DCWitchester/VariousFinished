using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
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
        public void SetDbfSaleFromSale(List<DBFSale> dBFSales, Int32 saleID, Int32 itemID)
        {
            //we iterate the current object Sale List
            foreach (var element in Vanzare)
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
}