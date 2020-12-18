using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using WebServiceEvidenta.VFPClasses;

namespace WebServiceEvidenta.WebServices
{
    /// <summary>
    /// Summary description for StorageService
    /// </summary>
    [WebService(Namespace = "http://www.mentorsoft.ro/WebAdministration/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StorageService : System.Web.Services.WebService
    {

        [WebMethod]
        public XmlDocument GetProductName(String ProductCode)
        {
            using (QuantityFileFunctions vfp = new QuantityFileFunctions())
            {
                return vfp.getProductName(ProductCode);
            }
        }

        [WebMethod]
        public void SetQuantityFile(String QuantityFile)
        {
            using (QuantityFileFunctions vfp = new QuantityFileFunctions())
            {
                vfp.SetQuantityFile(QuantityFile);
            }
        }
    }
}
