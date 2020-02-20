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
        /// <summary>
        /// this function will retrieve the number of lines contained by a string
        /// </summary>
        /// <param name="s">the number of lines</param>
        /// <returns></returns>
        public static Int32 RetriveNumberOfLinesOfString(String s)
        {
            return System.Text.RegularExpressions.Regex.Matches(s, System.Environment.NewLine).Count;
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
        /// <summary>
        /// the warning message displayed because pf a lack of settings file
        /// </summary>
        public static void MissingSettingsError()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            PosCeption.MessageForm newMessage = new PosCeption.MessageForm();
            newMessage.msgTitle.Content = "Eroare Lipsa Setari";
            newMessage.msgMessage.Content = "Fisierul de setari lipseste" + Environment.NewLine +
                                                "Doriti sa generati un fisier de setari nou?";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.warningImage));
            //set content for the buttons
            newMessage.btnCancel.Content = "Nu";
            newMessage.btnAccept.Content = "Da";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * MessageSettings.RetriveNumberOfLinesOfString(newMessage.msgMessage.Content.ToString());
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
        /// <summary>
        /// this info message will be displayed before Nom-Manual closing the program
        /// </summary>
        public static void ProgramWillNowClose()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            PosCeption.MessageForm newMessage = new PosCeption.MessageForm();
            newMessage.msgTitle.Content = "Atentie: Programul se va inchide acum.";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.infoImage));
            //deactivate the cancel button
            newMessage.btnCancel.Visibility = Visibility.Collapsed;
            //recenter and set the content of the Accept button
            newMessage.btnAccept.Margin = new Thickness(0, 0, 0, 10);
            newMessage.btnAccept.Content = "Ok";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height;
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
        /// <summary>
        /// this info message will be displayed before Nom-Manual closing the program
        /// </summary>
        public static void SettingsHaveBeenChanged()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            PosCeption.MessageForm newMessage = new PosCeption.MessageForm();
            newMessage.msgTitle.Content = "Atentie: Setari modificate";
            newMessage.msgMessage.Content = "Setarile de baza ale programului au fost modificate" + Environment.NewLine +
                                                "Acestea devin active la repornirea programului"+
                                                "Instanta aceasta de program se va inchide acum";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.infoImage));
            //deactivate the cancel button
            newMessage.btnCancel.Visibility = Visibility.Collapsed;
            //recenter and set the content of the Accept button
            newMessage.btnAccept.Margin = new Thickness(0, 0, 0, 10);
            newMessage.btnAccept.Content = "Ok";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * MessageSettings.RetriveNumberOfLinesOfString(newMessage.msgMessage.Content.ToString());
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
        /// <summary>
        /// this message will be called by an error on the field verification
        /// </summary>
        public static void FieldValidationError(String field)
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            PosCeption.MessageForm newMessage = new PosCeption.MessageForm();
            newMessage.msgTitle.Content = "Camp completat eronat";
            newMessage.msgMessage.Content = "Atentie campul "+field+" a fost completat eronat sau deloc." + Environment.NewLine +
                                                "Recompletati campul cu o valoare valida.";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.warningImage));
            //deactivate the cancel button
            newMessage.btnCancel.Visibility = Visibility.Collapsed;
            //recenter and set the content of the Accept button
            newMessage.btnAccept.Margin = new Thickness(0, 0, 0, 10);
            newMessage.btnAccept.Content = "Ok";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * MessageSettings.RetriveNumberOfLinesOfString(newMessage.msgMessage.Content.ToString());
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
        public static void FormValidationError()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            PosCeption.MessageForm newMessage = new PosCeption.MessageForm();
            newMessage.msgTitle.Content = "Atentie: Campuri Completate Necorespunzator";
            newMessage.msgMessage.Content = "Unul sau mai multe campuri obligatorii necompletate ." + Environment.NewLine +
                                                "Verificati campurile marcate cu rosu si reincercati.";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.errorImage));
            //deactivate the cancel button
            newMessage.btnCancel.Visibility = Visibility.Collapsed;
            //recenter and set the content of the Accept button
            newMessage.btnAccept.Margin = new Thickness(0, 0, 0, 10);
            newMessage.btnAccept.Content = "Ok";
            //Alters the height of the form as needed
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * MessageSettings.RetriveNumberOfLinesOfString(newMessage.msgMessage.Content.ToString());
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }

    }
}
