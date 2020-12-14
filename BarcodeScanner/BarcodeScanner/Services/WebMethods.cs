using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScanner.Services
{
    class WebMethods
    {
        public static String GetProductDetails => Settings.PublicSettings.WebServicePath + "/getProductName?ProductCode=";
        public static String SetProductQuantity => Settings.PublicSettings.WebServicePath + "/setProductQuantity?QuantityFile=";
    }
}
