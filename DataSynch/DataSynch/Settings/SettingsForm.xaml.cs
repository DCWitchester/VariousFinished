using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataSynch.Settings
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        #region FormEvents
        /// <summary>
        /// this function be used to move the window from the title bar
        /// </summary>
        /// <param name="sender">the caller object</param>
        /// <param name="e">the MouseButtonEvent</param>
        void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            Miscellaneous.Miscellaneous.MoveWindow(e, this);
        }
        /// <summary>
        /// this function will be used to close the form
        /// </summary>
        /// <param name="sender">the Elipse Button</param>
        /// <param name="e">the click Event</param>
        void FormClose(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// this function will be used to close the form
        /// </summary>
        /// <param name="sender">the caller object</param>
        /// <param name="e">the MouseButtonEvent</param>
        void FormClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// this function will check the GUID for the correct structure and check the format
        /// </summary>
        /// <param name="sender">the TextBoxes</param>
        /// <param name="e">the Preview Text Input event</param>
        private void GUID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((sender as TextBox) == this.tbClientGUID)
            {
                this.tbClientGUID.Text = FormatGUID(this.tbClientGUID.Text);
                this.tbClientGUID.SelectionStart = tbClientGUID.Text.Length;
            }
            else
            {
                this.tbWorkStationGUID.Text = FormatGUID(this.tbWorkStationGUID.Text);
                this.tbWorkStationGUID.SelectionStart = tbClientGUID.Text.Length;
            }
        }
        /// <summary>
        /// this function will be the main validation event for the textBoxes
        /// </summary>
        /// <param name="sender">the TextBox</param>
        /// <param name="e">the Validation Event</param>
        private void ValidateGUIDTextBox(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            Label lbl = new Label();
            if(tb == this.tbClientGUID) lbl = this.lblClientGuid;
            else lbl = this.lblWorkPointGuid;
            if (!ValidateTextBox(tb,lbl)) 
            {
                Message.Message.FieldValidationError(lbl.Content.ToString());
                tb.Focus();
            }
        }
        /// <summary>
        /// this function will be the main execution for the program
        /// </summary>
        /// <param name="sender">btnSaveSettings</param>
        /// <param name="e">the click event</param>
        void SaveSetting(object sender, RoutedEventArgs e)
        {
            Settings.ProgramSettings settings = new Settings.ProgramSettings();
            if(!(ValidateTextBox(this.tbClientGUID,this.lblClientGuid) && ValidateTextBox(this.tbWorkStationGUID, this.lblWorkPointGuid)))
            {
                Message.Message.FormValidationError();
                return;
            }
            RetrieveSettingsFromForm(settings);
            Settings.programSettings = settings;
            Settings.initializedSettings = true;
            this.Close();
        }
        #endregion
        #region Auxilliary
        /// <summary>
        /// this function will check if a string has a guid formating and if 
        /// </summary>
        /// <param name="guidString">the guid String</param>
        String FormatGUID(String guidString)
        {
            String guidComposition = "00000000-0000-0000-0000-000000000000";
            Int32[] guidLenght = guidComposition.Split('-').ToList().Select(x => x.Trim().Count()).ToArray();
            if (guidString.Length == guidLenght[0] ||
                guidString.Length == guidLenght[0] + guidLenght[1] + 1 ||
                guidString.Length == guidLenght[0] + guidLenght[1] + guidLenght[2] + 2 ||
                guidString.Length == guidLenght[0] + guidLenght[1] + guidLenght[2] + guidLenght[3] + 3)
            {
                guidString += '-';
            }
            return guidString;
        }
        /// <summary>
        /// this function will parse a given String to check if it is a valid GUID
        /// </summary>
        /// <param name="guidString">the given string</param>
        /// <returns>the validity of the GUID</returns>
        Boolean CheckGUID(String guidString)
        {
            return Guid.TryParse(guidString, out Guid result);
        }
        /// <summary>
        /// this function will validate a given textBox from the form and display an error
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        Boolean ValidateTextBox(TextBox textBox, Label label)
        {
            if (String.IsNullOrWhiteSpace(textBox.Text) || CheckGUID(textBox.Text))
            {
                label.Foreground = new SolidColorBrush(Colors.Red);
                textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                label.ClearValue(Control.ForegroundProperty);
                textBox.ClearValue(Border.BorderBrushProperty);
                return true;
            }
        }
        /// <summary>
        /// this function will dump the current input from the form into a Program settings Object
        /// </summary>
        /// <param name="settings">the Program Settings Object</param>
        void RetrieveSettingsFromForm(Settings.ProgramSettings settings)
        {
            settings.ClientGuid = this.tbClientGUID.Text;
            settings.WorkStationGuid = this.tbWorkStationGUID.Text;
            settings.DownloadInterval = Decimal.ToInt32(this.spDownload.Value);
            settings.UploadInterval = Decimal.ToInt32(this.spUpload.Value);
            settings.LastUpdateTime = DateTime.Now;
        }
        #endregion
    }
}
