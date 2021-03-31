using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using WebServicePOS.VFPClasses.POSTable;

namespace WebServicePOS.Services
{
    /// <summary>
    /// Summary description for PosTableService
    /// </summary>
    [WebService(Namespace = "http://www.mentorsoft.ro/WebPOS/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PosTableService : System.Web.Services.WebService
    {
        public PosTableService()
        {
            Setari.Settings.ReadSettings();
        }

        #region WebMenu
        [WebMethod]
        public XmlDocument GetMeniu()
        {
            using (PosTable vfp = new PosTable())
            {
                /*XmlDocument document = vfp.getMenu();*/
                return vfp.GetMenu();
            }
        }

        [WebMethod]
        public void SetSale(String sale)
        {
            //String xml = File.ReadAllText("D:\\xml.txt");
            using (PosTable vfp = new PosTable())
            {
                vfp.SetSale(sale.ToString());
            }
        }

        [WebMethod]
        public XmlDocument GetIsTableOpen(String table)
        {
            using (PosTable vfp = new PosTable())
            {
                return vfp.GetIsTableOpen(table);
            }
        }

        [WebMethod]
        public XmlDocument GetAdministration()
        {
            using (PosTable vfp = new PosTable())
            {
                return vfp.GetAdministrations();
            }
        }

        [WebMethod]
        public XmlDocument GetCategories()
        {
            using (PosTable vfp = new PosTable())
            {
                return vfp.GetCategories();
            }
        }

        [WebMethod]
        public XmlDocument GetTables()
        {
            using (PosTableWaiter vfp = new PosTableWaiter())
            {
                return vfp.GetTables();
            }
        }

        [WebMethod]
        public XmlDocument GetWaiter(String waiterCode)
        {
            using (PosTableWaiter vfp = new PosTableWaiter())
            {
                return vfp.GetWaiter(waiterCode);
            }
        }

        [WebMethod]
        public XmlDocument CheckWaiter(String waiterCode)
        {
            using (PosTableWaiter vfp = new PosTableWaiter())
            {
                return vfp.CheckWaiter(waiterCode);
            }
        }

        [WebMethod]
        public XmlDocument GetSaleOfTable(String tableCode)
        {
            using (PosTableWaiter vfp = new PosTableWaiter())
            {
                return vfp.GetSaleOfTable(tableCode);
            }
        }
        #endregion
    }
}
