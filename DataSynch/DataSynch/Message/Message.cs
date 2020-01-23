using System;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Message
{
    //settings for modifying the message form programatically
    public class MessageSettings
    {
        public static Int32 stringLineHeight = 15;              //setting for line height
        public static String missingFileTitle = String.Empty;   //setting for MissingFile title
        /// <summary>
        /// The MessageFormReturn property will dictate weatehr the message was exited with ok or cancel (true or false)
        /// </summary>
        public static bool messageFormReturn = false;           //setting for the return from window value: Used for 2-value forms
    }
    //path to message background Images <= all included in application
    public class MessageImages
    {
        public static String errorImage = @"images/error.jpg";
        public static String warningImage = @"images/warning.jpg";
        public static String infoImage = @"images/info.jpg";
        public static String succesImage = @"images/success.jpg";
    }
    //public messageTypes to be used later on
    public enum messageTypes
    {
        error = 1,
        warning,
        info,
        succes
    }
    class Message
    {
        
    }
}
