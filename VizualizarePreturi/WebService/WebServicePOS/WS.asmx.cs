using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;
using static WebServiceEvidenta.VFPClass;

namespace WebServicePOS
{
    /// <summary>
    /// Summary description for WS
    /// </summary>
    [WebService(Namespace = "http://www.mentorsoft.ro/WebPOS/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WS : System.Web.Services.WebService
    {
        [WebMethod]
        public XmlDocument getProductDisplay(String ProductCode)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                return vfp.getProductDetails(ProductCode);
            }
        }

        [WebMethod]
        public XmlDocument getProductName(String ProductCode)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                return vfp.getProductName(ProductCode);
            }
        }

        [WebMethod]
        public void setQuantityFile(String QuantityFile)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                vfp.SetQuantityFile(QuantityFile);
            }
        }
    }
}