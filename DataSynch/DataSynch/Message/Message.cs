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
        /// <summary>
        /// this function will Reinitialize the Form Return 
        /// </summary>
        public static void ReinitializeFormUnload()
        {
            messageFormReturn = false;
        }
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
        /// <summary>
        /// this error is used to display a mutex violation.
        /// </summary>
        public static void UniqueInstanceError()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            PosCeption.MessageForm newMessage = new PosCeption.MessageForm();
            newMessage.msgTitle.Content = "Eroare Instanta Unica";
            newMessage.msgMessage.Content = "Programul deja ruleaza." + Environment.NewLine + 
                                                "Se permite o unica instanta a programului." + Environment.NewLine + 
                                                "Aceasta instanta se va inchide.";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.errorImage));
            //deactivate the cancel button
            newMessage.btnCancel.Visibility = Visibility.Collapsed;
            //recenter and set the content of the Accept button
            newMessage.btnAccept.Margin = new Thickness(0, 0, 0, 10);
            newMessage.btnAccept.Content = "Ok";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * 3;
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
    }
}
