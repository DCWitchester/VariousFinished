using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicePOS.Classes
{
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