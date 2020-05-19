using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using TXTtoPDFv2.Messages;

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
        public static Int32 RetriveNumberOfLinesOfString(String s)
        {
            return Regex.Matches(s, System.Environment.NewLine).Count;
        }
    }
    //path to message background Images <= all included in application
    public class MessageImages
    {
        public static String errorImage = @"/Messages/images/error.jpg";
        public static String warningImage = @"/Messages/images/warning.jpg";
        public static String infoImage = @"/Messages/images/info.jpg";
        public static String succesImage = @"/Messages/images/success.jpg";
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
        public static void NoTextFileGiven()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Fisier text neselectat";
            newMessage.msgMessage.Content = "Atentie nu ati selectat un fisier text pentru conversie" + Environment.NewLine +
                                                "Selectati un fisier text si reincercati conversia catre pdf.";
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
        public static void ParametersError()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Eroare la parametri";
            newMessage.msgMessage.Content = "Atentie nu ati apelat procedura cu parametrii corespunzatori." + Environment.NewLine +
                                                "Syntaxa parametrilor este:" + Environment.NewLine +
                                                "cale_fisier [orientare] [dimensiune]";
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
        /// <summary>
        /// this error is used to display a mutex violation.
        /// </summary>
        public static void UniqueInstanceError()
        {
            //we call the Reinitialization of the unload variable before launching the form.
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
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
            newMessage.Height = newMessage.Height + MessageSettings.stringLineHeight * MessageSettings.RetriveNumberOfLinesOfString(newMessage.msgMessage.Content.ToString());
            //We need to force focus to this form
            newMessage.Focus();
            //we set the message as Topmost always
            newMessage.Topmost = true;
            //we use showDialog to await user input
            newMessage.ShowDialog();
        }
        /// <summary>
        /// initial date not selected
        /// </summary>
        public static void InitialDateNotSelected()
        {
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Eroare Data Initiala Neselectata";
            newMessage.msgMessage.Content = "Atentie, nu ati selectat data initiala." + Environment.NewLine +
                                                "Selectati data initiala si reincercati.";
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
        /// <summary>
        /// the final date not selected
        /// </summary>
        public static void FinalDateError()
        {
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Eroare Data Finala";
            newMessage.msgMessage.Content = "Atentie, data finala nu poate fi mai mica decat data initiala" + Environment.NewLine +
                                                "Recompletati datele si reincercati";
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
        public static void WrongPath()
        {
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Eroare Cale fisiere";
            newMessage.msgMessage.Content = "Atentie, calea fisierelor este eronata sau nu exista fisiere de log" + Environment.NewLine +
                                                "Reselectati calea si reincercati";
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
        public static void NoReportTypeSelected()
        {
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Tipul Raportului Neselectat";
            newMessage.msgMessage.Content = "Atentie, nu ati selectat tipul de raport." + Environment.NewLine +
                                                "Reselectati tipul raportului si reincercati";
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

        public static void ReportFinalized()
        {
            MessageSettings.ReinitializeFormUnload();
            MessageForm newMessage = new MessageForm();
            newMessage.msgTitle.Content = "Raport finalizat cu succes.";
            newMessage.msgMessage.Content = "Raportul a fost finalizat cu succes.";
            //change the background image
            newMessage.msgBackground.ImageSource = new BitmapImage(new Uri(@"pack://Application:,,," + MessageImages.succesImage));
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
