using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDisplay.WebFunctions
{
    public class WebMehtods
    {
        public static String GetProduct => Settings.PublicSettings.WebServicePath + "/getProductDisplay?ProductCode=";
    }
}
