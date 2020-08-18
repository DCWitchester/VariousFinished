using LocationDisplay.Miscellaneous;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.PublicObjects
{
    public class Settings
    {
        private static Boolean isUserLoggedIn { get; set; } = false;
        public static Boolean IsNavMenuHidden { 
            get => !isUserLoggedIn; 
        }
        public static Boolean IsUserLoggedIn
        {
            get => isUserLoggedIn;
            set => isUserLoggedIn = value;
        }

        private static Int32 animationSpeed { get; set; } = new Int32();
        public static Int32 AnimationSpeed 
        { 
            get => animationSpeed;
            set => animationSpeed = value;
        }

        public static DatabaseConnection.DatabaseController.GlobalConnectionSettings connectionSettings { get; set; } = new DatabaseConnection.DatabaseController.GlobalConnectionSettings();

        public static void ConsumeSettingsJSON(IConfiguration configuration)
        {
            Encryption encryption = new Encryption();
            Settings.AnimationSpeed = Convert.ToInt32(configuration["PublicSettings:ThreadSleep"]);
            Settings.connectionSettings.Database = encryption.Decrypt(configuration["PublicSettings:DatabaseSettings:Database"]);
            Settings.connectionSettings.Host = encryption.Decrypt(configuration["PublicSettings:DatabaseSettings:Host"]);
            Settings.connectionSettings.Password = encryption.Decrypt(configuration["PublicSettings:DatabaseSettings:Password"]);
            Settings.connectionSettings.Port = encryption.Decrypt(configuration["PublicSettings:DatabaseSettings:Port"]);
            Settings.connectionSettings.UserID = encryption.Decrypt(configuration["PublicSettings:DatabaseSettings:UserID"]);
        }
    }
    
}
