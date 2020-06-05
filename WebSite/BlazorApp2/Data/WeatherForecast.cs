using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlazorApp2.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
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
    }
}
