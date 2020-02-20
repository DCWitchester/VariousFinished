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
        void StartupPath(object sender, StartupEventArgs e) 
        {
            Settings.Settings.RetrieveSettingsFromFile();
            SystemTray.SystemTray.RunSystemTray();
        }
    }
}
