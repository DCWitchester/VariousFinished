using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServiceBergenbier
{
    /// <summary>
    /// The classes structure used for serialization and deserialization of the xml objects sent as a response
    /// </summary>
    public class Classes
    {
        /// <summary>
        /// the base structure used for serialization of a single recepe object
        /// </summary>
        public class Reteta
        {
            /// <summary>
            /// the product code
            /// </summary>
            [XmlAttribute]
            public String codp { get; set; }
            /// <summary>
            /// the product name
            /// </summary>
            [XmlAttribute]
            public String denm { get; set; }
        }
        /// <summary>
        /// the root object for the Retete Element
        /// </summary>
        [XmlRoot(ElementName = "Retete")]
        public class Retete
        {
            /// <summary>
            /// the base list element for the Retete element List
            /// </summary>
            [XmlElement("Reteta")]
            public List<Reteta> retete { get; set; } = new List<Reteta>();

            /// <summary>
            /// the main class for filling the object from the dataTable
            /// </summary>
            /// <param name="dt">the DataTable retrieved from P2/P3</param>
            public void setReteteFromDataTable(DataTable dt)
            {
                //we have to iterate all elements in the dataTable row by row
                foreach(DataRow element in dt.Rows)
                {
                    //then initialize a new element for the row and add it to the list
                    retete.Add(new Reteta { 
                        codp = element[0].ToString().Trim(), 
                        denm = element[1].ToString().Trim() }
                    );
                }
            }
        }
        /// <summary>
        /// the base list element for the base recepe element with vote count
        /// </summary>
        public class Count
        {
            /// <summary>
            /// the product code
            /// </summary>
            [XmlAttribute]
            public String codp { get; set; }
            /// <summary>
            /// the product name
            /// </summary>
            [XmlAttribute]
            public String denm { get; set; }
            /// <summary>
            /// the curent vote count
            /// </summary>
            [XmlAttribute]
            public Int32 votecount { get; set; }
        }
        /// <summary>
        /// the base serialization lsit element
        /// </summary>
        [XmlRoot(ElementName = "Counts")]
        public class Counts
        {
            /// <summary>
            /// the main count list
            /// </summary>
            [XmlElement("Count")]
            public List<Count> counts { get; set; } = new List<Count>();

            /// <summary>
            /// the initialization of the main list item from a data table
            /// </summary>
            /// <param name="dt">the main data table</param>
            public void setReteteFromDataTable(DataTable dt)
            {
                //we iterate all the rows of the data table
                foreach (DataRow element in dt.Rows)
                {
                    //and then create a new element for each row and add it to the list
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