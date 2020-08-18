using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTable
{
    /// <summary>
    /// the Main Class for containing WebMethods
    /// </summary>
    public class WebMethods
    {
        /// <summary>
        /// this String will retrieve the getMenu WebService adress
        /// </summary>
        public static String GetMenu => Settings.PublicSettings.WebServicePath + "/getMeniu";
        /// <summary>
        /// this String will retrieve the setSale WebService adress and prepare it for receiving the parameter
        /// </summary>
        public static String SetSales => Settings.PublicSettings.WebServicePath + "/setSale?sale=";
        /// <summary>
        /// this String will retrieve the getIsTableOpen WebService adress and prepare it for receiving the parameter
        /// </summary>
        public static String GetIsTableOpen => Settings.PublicSettings.WebServicePath + "/getIsTableOpen?table=";
        /// <summary>
        /// this String will retrieve the getAdministrations WebService adress
        /// </summary>
        public static String GetAdministrations => Settings.PublicSettings.WebServicePath + "/getAdministration";
        /// <summary>
        /// this String will retrieve the getCategories WebService adress
        /// </summary>
        public static String GetCategories => Settings.PublicSettings.WebServicePath + "/getCategories";
    }
}
