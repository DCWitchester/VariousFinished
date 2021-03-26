using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace WebServicePOS.WebServiceFunctionality
{
    public static class DataTable2XmlDocument
    {
        static public XmlDocument Convert(DataTable tabela, String mesajEroare)
        {
            XmlDocument result = new XmlDocument();
            using (StringWriter sw = new StringWriter())
            {
                if (tabela == null)
                {
                    String mE = mesajEroare.Replace('\\', ' ').Replace('\'', ' ').Replace('"', ' ').Replace('<', ' ').Replace('>', ' ');
                    result.LoadXml("<DocumentElement><EROAREFATALA><MESAJ>" + mE + "</MESAJ></EROAREFATALA></DocumentElement>");
                }
                else
                {
                    tabela.WriteXml(sw);
                    result.LoadXml(sw.ToString());
                }
            }
            return result;
        }
    }
}