using System;
using System.Web.Services;
using System.Xml;
using WebServiceEvidenta.VFPClasses;

namespace WebServiceEvidenta.WebServices
{
    /// <summary>
    /// Summary description for ProductDisplay
    /// </summary>
    [WebService(Namespace = "http://www.mentorsoft.ro/WebAdministration/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductDisplay : System.Web.Services.WebService
    {
        [WebMethod]
        public XmlDocument GetProductDisplay(String ProductCode)
        {
            using (ProductDisplayFunctions vfp = new ProductDisplayFunctions())
            {
                return vfp.getProductDetails(ProductCode);
            }
        }
    }
}
