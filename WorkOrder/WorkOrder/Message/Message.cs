using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static WorkOrder.Messages.MessageSettings;

namespace WorkOrder.Messages
{
    class MessageSettings
    {
        public static Int32 stringLineHeight = 15;              //setting for line height
        public static String missingFileTitle = String.Empty;   //setting for MissingFile title
        /// <summary>
        /// the current entry for the messageFormReturn
        /// </summary>
        public static Boolean messageFormReturn { get; set; } = false;
        /// <summary>
        /// this fucntion will reinitialize the FormReturn to make sure that it is false
        /// </summary>
        public static void ReinitializeFormUnload()
        {
            messageFormReturn = false;
        }
        //path to message background Images <= all included in application
        public class MessageImages
        {
            public static String errorImage = @"/../img/error.jpg";
            public static String warningImage = @"/../img/warning.jpg";
            public static String infoImage = @"/../img/info.jpg";
            public static String succesImage = @"/../img/success.jpg";
        }
        //public messageTypes to be used later on
        public enum messageTypes
        {
            error = 1,
            warning,
            info,
            succes
        }
    }
    class Message
    {
        public static void StartupError()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Program deja pornit";
            newMessage.msgMessage.Content = "Exista deja o instanta activa a programului." + Environment.NewLine + "Instanta curenta a programului se va inchide.";
            //change the background image
            newMessage.msgBackground.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.errorImage));
            //deactivate the cancel button
            newMessage.btnCancel.Visibility = Visibility.Collapsed;
            //recenter and set the content of the Accept button
            newMessage.btnAccept.Margin = new Thickness(0, 0, 0, 10);
            newMessage.btnAccept.Content = "Ok";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * 2;
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
    }
}
