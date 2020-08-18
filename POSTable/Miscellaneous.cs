using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTable
{
    //The Miscellaneous functions will contain all global use functions
    public class Miscellaneous
    {
        /// <summary>
        /// this function will set the Background Image for the webSite
        /// </summary>
        /// <returns>the background image</returns>
        public static String SetBackgroundImage()
        {
            switch (Settings.PublicSettings.DisplayMode)
            {
                case Settings.DisplayMode.Night:
                    return "url('images/menu_background_night.jpg')";
                case Settings.DisplayMode.Light:
                    return "url('images/menu_background_light.jpg')";
                case Settings.DisplayMode.Picnic:
                    return "url('images/menu_background_picnic.jpg')";
                default:
                    return "url('images/menu_background_night.jpg')";
            }
        }

        /// <summary>
        /// this function will set the foreground color to the webSite
        /// </summary>
        /// <returns>the color string</returns>
        public static String SetForegroundColor()
        {
            switch (Settings.PublicSettings.DisplayMode)
            {
                case Settings.DisplayMode.Light:
                    return "black";
                default:
                    return "white";
            }
        }

        /// <summary>
        /// this function will set the currency to the price item
        /// </summary>
        /// <returns>the current currency for the price</returns>
        public static String SetCurrency()
        {
            switch (Settings.PublicSettings.Currency)
            {
                case Settings.Currency.Euro:
                    return "€";
                case Settings.Currency.Dollar:
                    return "$";
                default:
                    return "Lei";
            }
        }
    }
}
