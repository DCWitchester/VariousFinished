using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace TXTtoPDFv2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// the main startup Procedure for the program
        /// </summary>
        /// <param name="sender">the program caller</param>
        /// <param name="e">the startup event arguments passed to the program by the starting event</param>
        void ProgramStartup(object sender, StartupEventArgs e)
        {
            //as always to avoid bad ruling we set the program culture to Englis International
            CultureInfo.CurrentCulture = new CultureInfo("en-IN");
            CultureInfo.CurrentUICulture = new CultureInfo("en-IN");
            //if no arguments have been passed or the first argument is an empty error we display a message and That's All Folks
            if (e.Args.Count() == 0 || String.IsNullOrWhiteSpace(e.Args[0])) Message.Message.ParametersError();
            else
            {

                //if we have arguments we initialize a new instance of the page settings class
                PageSettings pageSettings = new PageSettings();
                //set the document to the first parameter
                String textDocument = e.Args[0];
                //set the settings file name by replacing the extension from a .txt to .pdf
                pageSettings.FileName = textDocument.Trim().Replace(".txt", ".pdf");
                //then see if i was told the orientaion of the pdf document
                pageSettings.DocumentType = (e.Args[1] ?? "P") == "L" ? PDFGenerator.DocumentType.Landscape : PDFGenerator.DocumentType.Portrait;
                //and the font size
                pageSettings.FontSize = Convert.ToDouble(e.Args[2] ?? "8");
                //also in order to be able to bind a key event we register the hotKey
                HotKeyManager.RegisterHotKey(System.Windows.Forms.Keys.Escape,KeyModifiers.None);
                //and give it a pressing event
                HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(CloseProgram);
                //window.GenerateDocument(textDocument, pageSettings);
                //before finally generating the document
                PDFGenerator.GenerateDocument(textDocument, pageSettings);
            }
            //we finally close the program
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        /// <summary>
        /// the main function for closing both the program and the PDF file
        /// </summary>
        /// <param name="sender">the main object sender: HotKey Keypress</param>
        /// <param name="e">the hotkey press event for the binded key</param>
        void CloseProgram(object sender, HotKeyEventArgs e)
        {
            System.Diagnostics.Process.GetProcessById(PDFGenerator.ProcessID).Kill();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        /*
        PageSettings pageSettings = new PageSettings();
            if (String.IsNullOrWhiteSpace(this.tbFilePath.Text.Trim()))
            {
            Message.Message.NoTextFileGiven();
            return;
        }
        String textDocument = this.tbFilePath.Text.Trim();
            pageSettings.FileName = this.tbFilePath.Text.Trim().Replace(".txt", ".pdf");
        pageSettings.DocumentType = (this.rbLandscape.IsChecked ?? false) ? PDFGenerator.DocumentType.Landscape : PDFGenerator.DocumentType.Portrait;
        PDFGenerator.GenerateDocument(textDocument, pageSettings);*/
    }
}
