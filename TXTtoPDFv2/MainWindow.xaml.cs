using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace TXTtoPDFv2
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const Int32 MOD_NONE    = (Int32)Keys.None; //[NONE]
        private const Int32 MOD_ALT     = (Int32)Keys.Alt; //ALT
        private const Int32 MOD_CONTROL = (Int32)Keys.Control; //CTRL
        private const Int32 MOD_SHIFT   = (Int32)Keys.Shift; //SHIFT
        private const Int32 MOD_WIN     = 0x0008; //WINDOWS
        private const Int32 VK_CAPITAL  = (Int32)Keys.CapsLock;//CAPS LOCK:


        private HwndSource source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr handle = new WindowInteropHelper(this).Handle;
            source = HwndSource.FromHwnd(handle);
            source.AddHook(HwndHook);

            RegisterHotKey(handle, HOTKEY_ID, MOD_NONE, (Int32)Keys.Escape); //CTRL + CAPS_LOCK
        }
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            System.Diagnostics.Process.GetProcessById(PDFGenerator.ProcessID).Kill();
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// this function will be used to move the form from the title bar
        /// </summary>
        /// <param name="sender">the title bar</param>
        /// <param name="e">the MouseDown Event</param>
        public void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        /// <summary>
        /// this function will be used to close the program from the image elipse
        /// </summary>
        /// <param name="sender">the image elipse</param>
        /// <param name="e">the MouseUp Event</param>
        public void CloseProgram(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        /// <summary>
        /// this function will be used to select the
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectDirectoryFolder(object sender,RoutedEventArgs e)
        {

            //we instantiate a new windowDialog for searching for a file
            SaveFileDialog windowDialog = new SaveFileDialog();
            //set the default extension to .txt
            windowDialog.DefaultExt = ".txt";
            //create a filter for textDocuments
            windowDialog.Filter = "Text documents (.txt)|*.txt";
            //and then display the fileDialog
            //finally if a file has been selected we place the file name in the path
            if (windowDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) this.tbFilePath.Text = windowDialog.FileName;
            windowDialog.Dispose();
        }
        public void GenerateDocument(object sender,RoutedEventArgs e)
        {
            PageSettings pageSettings = new PageSettings();
            if (String.IsNullOrWhiteSpace(this.tbFilePath.Text.Trim()))
            {
                Message.Message.NoTextFileGiven();
                return;
            }
            String textDocument = this.tbFilePath.Text.Trim();
            pageSettings.FileName = this.tbFilePath.Text.Trim().Replace(".txt", ".pdf");
            pageSettings.DocumentType = (this.rbLandscape.IsChecked ?? false) ? PDFGenerator.DocumentType.Landscape : PDFGenerator.DocumentType.Portrait;
            PDFGenerator.GenerateDocument(textDocument, pageSettings);
        }
    }
}
