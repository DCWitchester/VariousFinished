using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebServicePOS.Auxiliary;
using WebServicePOS.WebServiceFunctionality;

namespace WebServicePOS.Setari
{
    public static class Settings
    {
        #region Properties
        public static string caleEcashFDB = @"C:\Programe\Ecash\Ecash.FDB";
        public static string userFirebird = "SYSDBA";
        public static string passFirebird = "masterkey";
        public static string calePOS = @"C:\Programe\Vmentor\DATE";
        public static bool tokenInSettingsIni = false;
        public static string token = "token";
        public static string notificationURL = "";
        #endregion

        /// <summary>
        /// this function will check the given token
        /// </summary>
        /// <param name="tk">the given token</param>
        /// <returns>if the given token was corect or not</returns>
        public static bool TokenIsOk(string tk)
        {
            return ((token + "42x").CompareTo(tk.Trim()) == 0);
        }

        /// <summary>
        /// this function will Read the Settings from the server
        /// </summary>
        public static void ReadSettings()
        {
            string Setari = HttpContext.Current.Server.MapPath("setari.ini").Replace("\\Services","");
            if (File.Exists(Setari))
            {
                using (StreamReader sr = new StreamReader(Setari))
                {
                    Codificator navajo = new Codificator();
                    Settings.caleEcashFDB = navajo.Decrypt(sr.ReadLine());
                    Settings.userFirebird = navajo.Decrypt(sr.ReadLine());
                    Settings.passFirebird = navajo.Decrypt(sr.ReadLine());
                    Settings.calePOS = navajo.Decrypt(sr.ReadLine());
                    if (!sr.EndOfStream)
                    {
                        Settings.token = navajo.Decrypt(sr.ReadLine());
                        Settings.token = (Settings.tokenInSettingsIni = Settings.token.Contains("=")) ? Settings.token.Substring(Settings.token.IndexOf("=") + 1).Trim() : "token";
                    }
                    if (!Settings.tokenInSettingsIni) { using (EPay epay = new EPay()) { Settings.token = epay.GetToken(); } }
                }
            }
            string notificari = HttpContext.Current.Server.MapPath("notificari.ini");
            if (File.Exists(notificari)) { using (StreamReader sr = new StreamReader(notificari)) { Settings.notificationURL = sr.ReadLine(); } }
        }
    }
}