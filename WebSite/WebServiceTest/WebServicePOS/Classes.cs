using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServiceBergenbier
{
    public class Classes
    {
        
        public class Reteta
        {
            [XmlAttribute]
            public String codp { get; set; }
            [XmlAttribute]
            public String denm { get; set; }
        }
        [XmlRoot(ElementName = "Retete")]
        public class Retete
        {
            [XmlElement("Reteta")]
            public List<Reteta> retete { get; set; } = new List<Reteta>();

            public void setReteteFromDataTable(DataTable dt)
            {
                foreach(DataRow element in dt.Rows)
                {
                    retete.Add(new Reteta { 
                        codp = element[0].ToString().Trim(), denm = element[1].ToString().Trim() }
                    );
                }
            }
        }
        
    }
}