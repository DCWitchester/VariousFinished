using Microsoft.Extensions.Configuration;
using ProductDisplay.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDisplay.Settings
{
    public class PublicSettings
    {
        public static String WebServicePath { get; set; } = String.Empty;

        public static void ConsumeSettingsJson(IConfiguration configuration)
        {
            Encrypter encrypter = new Encrypter();
            WebServicePath = encrypter.Decrypt(configuration["PublicSettings:WebServicePath"]);
        }
    }
}
