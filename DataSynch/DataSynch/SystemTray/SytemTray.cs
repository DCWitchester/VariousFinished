using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataSynch.SystemTray
{
    class SystemTray
    {
        public static void RunSystemTray()
        {
            Auxilliary.GenerateInstance();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                STAApplicationContext context = new STAApplicationContext();
                Application.Run(context);
            }
            catch { }
        }
    }
    public class STAApplicationContext : ApplicationContext
    {
        public STAApplicationContext()
        {
            _deviceManager = new Auxilliary.DeviceManager();
            _viewManager = new ViewManager(_deviceManager);

            _deviceManager.OnStatusChange += _viewManager.OnStatusChange;

            _deviceManager.Initialize();
        }

        private ViewManager _viewManager;
        private Auxilliary.DeviceManager _deviceManager;

        // Called from the Dispose method of the base class
        protected override void Dispose(bool disposing)
        {
            if ((_deviceManager != null) && (_viewManager != null))
            {
                _deviceManager.OnStatusChange -= _viewManager.OnStatusChange;
            }
            if (_deviceManager != null)
            {
                _deviceManager.Terminate();
            }
            _deviceManager = null;
            _viewManager = null;
        }
    }
    /// <summary>
    /// this class will contain all auxilliary functions for the systemTray
    /// </summary>
    class Auxilliary
    {
        public static void GenerateInstance()
        {
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString(), out Boolean createdNew))
            {
                if (!createdNew)
                {
                    Message.Message.UniqueInstanceError();
                    Miscellaneous.Miscellaneous.ProgramClose();
                }
            }
        }
        #region DeviceManager
        /// <summary>
        /// the dummy device for state control
        /// </summary>
        public interface IDeviceManager
        {
            string DeviceName { get; }
            DeviceStatus Status { get; }
            List<KeyValuePair<string, bool>> StatusFlags { get; }
            void Initialize();
            void Start();
            void Stop();
            void Terminate();
        }
        /// <summary>
        /// the Device Status used for error displaying
        /// </summary>
        public enum DeviceStatus
        {
            Uninitialised,
            Initialised,
            Starting,
            Running,
            Paused,
            Error,
        }
        /// <summary>
        /// the main extension of the IDeviceManager
        /// </summary>
        public class DeviceManager : IDeviceManager
        {
            //this is actually well done and cool so my alterations will be minimal
            /// <summary>
            /// the class initialization
            /// </summary>
            public DeviceManager()
            {
                Status = DeviceStatus.Uninitialised;
                //i need the program to start on runtime
                Start();
            }

            //a status timer used only for event simulation for now?? <=Weird but useful for further development
            private System.Windows.Threading.DispatcherTimer _statusTimer;

            private void KillTimer()
            {
                if (_statusTimer != null)
                {
                    _statusTimer.Stop();
                    _statusTimer = null;
                }
            }

            //Main delegate for status change <= used for refresh purposes
            public delegate void StatusChangeEvent();
            //main event for status change <= used for refresh purposes
            public event StatusChangeEvent OnStatusChange;

            //The name for the app
            public string DeviceName { get; private set; }
            //the device status
            public DeviceStatus Status { get; private set; }

            /// <summary>
            /// the list key pairing for the device status
            /// </summary>
            public List<KeyValuePair<string, bool>> StatusFlags
            {
                get
                {
                    List<KeyValuePair<string, bool>> statusFlags = new List<KeyValuePair<string, bool>>();

                    switch (Status)
                    {
                        case DeviceStatus.Running:
                            statusFlags.Add(new KeyValuePair<string, bool>("Transferul de date este activ", true));
                            break;
                        case DeviceStatus.Error:
                            statusFlags.Add(new KeyValuePair<string, bool>("Eroare la transfer", true));
                            break;
                        case DeviceStatus.Uninitialised:
                            statusFlags.Add(new KeyValuePair<string, bool>("Transferul de date este inactiv", true));
                            break;
                        case DeviceStatus.Initialised:
                            statusFlags.Add(new KeyValuePair<string, bool>("Transferul de date este in curs de pornire", true));
                            break;
                        case DeviceStatus.Paused:
                            statusFlags.Add(new KeyValuePair<string, bool>("Transferul de date este oprit temporar", true));
                            break;
                    }
                    return statusFlags;
                }
            }

            /// <summary>
            /// the initialization of the Program
            /// </summary>
            public void Initialize()
            {
                if (Status == DeviceStatus.Uninitialised)
                {
                    Status = DeviceStatus.Initialised;
                }

                DeviceName = "Mentor DataSynch";
            }

            /// <summary>
            /// the startup of the program
            /// </summary>
            public void Start()
            {
                if (Status == DeviceStatus.Initialised)
                {
                    Status = DeviceStatus.Starting;
                    // Simulate a real device with a simple timer
                    _statusTimer = new System.Windows.Threading.DispatcherTimer(
                        new TimeSpan(0, 0, 3),
                        System.Windows.Threading.DispatcherPriority.Normal,
                        delegate
                        {
                            KillTimer();
                            Status = DeviceStatus.Running;
                            _statusTimer = null;
                            if (OnStatusChange != null)
                            {
                                OnStatusChange();
                            }
                        },
                        System.Windows.Threading.Dispatcher.CurrentDispatcher);

                }
            }

            /// <summary>
            /// the stopping of the program 
            /// </summary>
            public void Stop()
            {
                if (Status == DeviceStatus.Running)
                {
                    Status = DeviceStatus.Initialised;
                    if (OnStatusChange != null)
                    {
                        OnStatusChange();
                    }
                }
            }

            /// <summary>
            /// the main function for terminating execution
            /// </summary>
            public void Terminate()
            {
                KillTimer();
                Stop();
                Status = DeviceStatus.Uninitialised;
            }

            /// <summary>
            /// the main function for the error calling
            /// </summary>
            public void Error()
            {
                KillTimer();
                Stop();
                Status = DeviceStatus.Error;

            }

            /// <summary>
            /// the main function for pausing the execution thread
            /// </summary>
            public void Pause()
            {
                if (Status == DeviceStatus.Running)
                {
                    Status = DeviceStatus.Paused;
                    if (OnStatusChange != null)
                    {
                        OnStatusChange();
                    }
                }
            }
        }
        #endregion
    }


    class ViewManager
    {
        #region PrivateProperties
        // The Windows system tray class
        private NotifyIcon _notifyIcon;
        private Auxilliary.DeviceManager _deviceManager;

        // This allows code to be run on a GUI thread
        private System.Windows.Window _hiddenWindow;
        private System.ComponentModel.IContainer _components;

        //private DataSynch.SystemTray.ViewModel.View.AboutView _aboutView;
        private ViewModel.AboutViewModel _aboutViewModel;
        private ViewModel.StatusViewModel _statusViewModel;
        System.Windows.Media.ImageSource AppIcon
        {
            get
            {
                System.Drawing.Icon icon = Properties.Resources.MENTOR;
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
        }
        #endregion

        public ViewManager(Auxilliary.DeviceManager deviceManager)
        {
            System.Diagnostics.Debug.Assert(deviceManager != null);

            _deviceManager = deviceManager;

            _components = new System.ComponentModel.Container();

            _notifyIcon = new System.Windows.Forms.NotifyIcon(_components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.MENTOR,
                Text = "Mentor DataSynch",
                Visible = true,
            };

            _notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            //_notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            //_notifyIcon.MouseUp += notifyIcon_MouseUp;
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;

            if (_notifyIcon.ContextMenuStrip.Items.Count == 0)
            {
                //the ToolStrip Menu Item for pausing execution
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Pauza Transferul", "Se pune pauza la transferul de date", startStopReaderItem_Click));

                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Reincepe Transferul", "Se reincepe transferul de date", startStopReaderItem_Click));
                
                //the Separator for the Strip Menu Item
                _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Iesire", "Inchiderea programului de catre administrator", closeProgramFromTray));
                /*
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Code Project &Web Site", "Navigates to the Code Project Web Site", showWebSite_Click));
                _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                */
            }

            SetMenuItems();
        }
        /// <summary>
        /// the menu item generator for the menu class
        /// is dependent on the current systray status
        /// </summary>
        private void SetMenuItems()
        {
            switch (_deviceManager.Status)
            {
                case Auxilliary.DeviceStatus.Initialised:
                    break;
                case Auxilliary.DeviceStatus.Starting:
                    break;
                case Auxilliary.DeviceStatus.Running:
                    break;
                case Auxilliary.DeviceStatus.Uninitialised:
                    break;
                case Auxilliary.DeviceStatus.Error:
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "SetButtonStatus() => Unknown state");
                    break;
            }
        }
        /// <summary>
        /// Creates a new ToolStrip Menu Item with a name, toolTip and Atached Event
        /// </summary>
        /// <param name="displayText">The Title Text for the ToolStrip item</param>
        /// <param name="tooltipText">the ToolTip for the ToolStrip item</param>
        /// <param name="eventHandler">the Handler for the event</param>
        /// <returns></returns>
        private ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, string tooltipText, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null)
            {
                item.Click += eventHandler;
            }

            item.ToolTipText = tooltipText;
            return item;
        }
        #region ToolStripEvents
        /// <summary>
        /// the Main Event for initializing the status Change of the project
        /// </summary>
        /// <param name="sender">the System Tray Menu</param>
        /// <param name="e">the Click Event</param>
        private void startStopReaderItem_Click(object sender, EventArgs e)
        {
            if (_deviceManager.Status == Auxilliary.DeviceStatus.Running)
            {
                _deviceManager.Pause();
            }
            else
            {
                _deviceManager.Start();
            }
        }
        public void OnStatusChange()
        {
            UpdateStatusView();

            switch (_deviceManager.Status)
            {
                case Auxilliary.DeviceStatus.Initialised:
                    _notifyIcon.Text = _deviceManager.DeviceName + ": Ready";        
                    DisplayStatusMessage("Idle");
                    break;
                case Auxilliary.DeviceStatus.Running:
                    _notifyIcon.Text = _deviceManager.DeviceName + ": Running";
                    DisplayStatusMessage("Running");
                    break;
                case Auxilliary.DeviceStatus.Starting:
                    _notifyIcon.Text = _deviceManager.DeviceName + ": Starting";
                    DisplayStatusMessage("Starting");
                    break;
                case Auxilliary.DeviceStatus.Uninitialised:
                    _notifyIcon.Text = _deviceManager.DeviceName + ": Not Ready";
                    break;
                case Auxilliary.DeviceStatus.Error:
                    _notifyIcon.Text = _deviceManager.DeviceName + ": Error Detected";
                    break;
                default:
                    _notifyIcon.Text = _deviceManager.DeviceName + ": -";
                    break;
            }
            System.Windows.Media.ImageSource icon = AppIcon;
        }
        private void UpdateStatusView()
        {
            if ((_statusViewModel != null) && (_deviceManager != null))
            {
                List<KeyValuePair<string, bool>> flags = _deviceManager.StatusFlags;
                List<KeyValuePair<string, string>> statusItems = flags.Select(n => new KeyValuePair<string, string>(n.Key, n.Value.ToString())).ToList();
                statusItems.Insert(0, new KeyValuePair<string, string>("Device", _deviceManager.DeviceName));
                statusItems.Insert(1, new KeyValuePair<string, string>("Status", _deviceManager.Status.ToString()));
                _statusViewModel.SetStatusFlags(statusItems);
            }
        }
        private void DisplayStatusMessage(string text)
        {
            _hiddenWindow.Dispatcher.Invoke(delegate
            {
                _notifyIcon.BalloonTipText = _deviceManager.DeviceName + ": " + text;
                // The timeout is ignored on recent Windows
                _notifyIcon.ShowBalloonTip(3000);
            });
        }

        private void closeProgramFromTray(object sender, EventArgs e)
        {
            #warning TBD: Exit from System Tray
        }
        #endregion
    }
}
