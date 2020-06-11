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
    /// <summary>
    /// the main Reteta Class for deserialization of the xml
    /// </summary>
    public class Reteta
    {
        [XmlAttribute]
        public String codp { get; set; }
        [XmlAttribute]
        public String denm { get; set; }
    }
    /// <summary>
    /// the main root Retete Class for deserialization of the xml
    /// </summary>
    [XmlRoot(ElementName = "Retete")]
    public class Retete
    {
        [XmlElement("Reteta")]
        public List<Reteta> retete { get; set; } = new List<Reteta>();
    }
    /// <summary>
    /// the main Count Class for deserialization of the xml
    /// </summary>
    public class Count
    {
        [XmlAttribute]
        public String codp { get; set; }
        [XmlAttribute]
        public String denm { get; set; }
        [XmlAttribute]
        public Int32 votecount { get; set; }
    }
    /// <summary>
    /// the main root Counts Class for deserialization of the xml
    /// </summary>
    [XmlRoot(ElementName = "Counts")]
    public class Counts
    {
        [XmlElement("Count")]
        public List<Count> counts { get; set; } = new List<Count>();
    }
}
