using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace DataSynch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// the main Startup Event for the Main 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StartupPath(object sender, StartupEventArgs e)
        {
            //the creation of the vfpFunction List
            FoxResources.VFPIntegration.CreateFunctionsList();
            String step = "";
            FoxResources.VFPCaller.RetriveProducts();
            String t = "";
            //first we will retrieve the program settings from the settings file
            Settings.Settings.RetrieveSettingsFromFile();
            //then we will retrieve the clients and workStation Settings
            Settings.ServerSettings.ClientSettings.RetrieveServerSettings();
            //lastly we will run the system tray
            SystemTray.SystemTray.RunSystemTray();
        }
    }
}
