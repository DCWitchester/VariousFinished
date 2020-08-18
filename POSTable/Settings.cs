using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTable
{
    /// <summary>
    /// the settings class will contain this Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// the Display Mode Enum
        /// </summary>
        public enum DisplayMode
        {
            //Night Mode
            Night = 1,
            //Light Mode
            Light = 2,
            //Night Mode + Picnic Background
            Picnic = 3
        }
        /// <summary>
        /// the Currency settings
        /// </summary>
        public enum Currency
        {
            RON = 1,
            Euro = 2,
            Dollar = 3
        }
        /// <summary>
        /// this class will contain global static settings
        /// </summary>
        public class PublicSettings
        {
            //the webService Path
            public static String WebServicePath { get; set; }
            //the programs default display Mode
            public static DisplayMode DisplayMode { get; set; }
            //the currency displayed with the price
            public static Currency Currency { get; set; }
        }
    }
}
