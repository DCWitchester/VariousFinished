using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the XmlRoot Element for Simple Boolean Answers
    /// </summary>
    [XmlRoot(ElementName = "Raspuns")]
    public class Answer
    {
        /// <summary>
        /// the Answer Value
        /// </summary>
        public Boolean Valoare { get; set; }
    }
}