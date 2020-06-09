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
        public class Count
        {
            [XmlAttribute]
            public String codp { get; set; }
            [XmlAttribute]
            public String denm { get; set; }
            [XmlAttribute]
            public Int32 votecount { get; set; }
        }
        [XmlRoot(ElementName = "Counts")]
        public class Counts
        {
            [XmlElement("Count")]
            public List<Count> counts { get; set; } = new List<Count>();

            public void setReteteFromDataTable(DataTable dt)
            {
                foreach (DataRow element in dt.Rows)
                {
                    counts.Add(new Count
                    {
                        codp = element[0].ToString().Trim(),
                        denm = element[1].ToString().Trim(),
                        votecount = Convert.ToInt32(element[2])
                    }
                    );
                }
            }
        }

    }
}