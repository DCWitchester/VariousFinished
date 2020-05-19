using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace TXTtoPDFv2
{
    /// <summary>
    /// the main static class for the HotKeyManager
    /// </summary>
    public static class HotKeyManager
    {
        /// <summary>
        /// the Central Event Handler
        /// </summary>
        public static event EventHandler<HotKeyEventArgs> HotKeyPressed;

        /// <summary>
        /// the main function for registering a HotKey
        /// </summary>
        /// <param name="key">the Key to be bound</param>
        /// <param name="modifiers">any given Modifier</param>
        /// <returns></returns>
        public static int RegisterHotKey(Keys key, KeyModifiers modifiers)
        {
            //we block the current thread to stop error handling
            _windowReadyEvent.WaitOne();
            //we initialiaze a new id for the event
            int id = System.Threading.Interlocked.Increment(ref _id);
            //then invoke the HotKeyManager with the needed parameters forcing the event id
            _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, id, (uint)modifiers, (uint)key);
            //and return the id
            return id;
        }

        /// <summary>
        /// the main function for unregistering a registered HotKey
        /// </summary>
        /// <param name="id">the HotKey ID</param>
        public static void UnregisterHotKey(int id)
        {
            _wnd.Invoke(new UnRegisterHotKeyDelegate(UnRegisterHotKeyInternal), _hwnd, id);
        }

        /// <summary>
        /// the main delegate for registering a Hotkey under Windows
        /// </summary>
        /// <param name="hwnd">the HotKey pointer Handler</param>
        /// <param name="id">the HotKey PressEvent ID</param>
        /// <param name="modifiers">the active input key modifier</param>
        /// <param name="key">the active input key</param>
        delegate void RegisterHotKeyDelegate(IntPtr hwnd, int id, uint modifiers, uint key);
        /// <summary>
        /// the main delegate for unregistering an !!ALREADY REGISTERED!! HOTKEY
        /// </summary>
        /// <param name="hwnd">the HotKey KeyPress Handler</param>
        /// <param name="id">the HotKey ID</param>
        delegate void UnRegisterHotKeyDelegate(IntPtr hwnd, int id);

        /// <summary>
        /// the main function for registering a Hotkey under Windows
        /// </summary>
        /// <param name="hwnd">the HotKey pointer Handle</param>
        /// <param name="id">the HotKey PressEvent ID</param>
        /// <param name="modifiers">the active input key modifier</param>
        /// <param name="key">the active input key</param>
        private static void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
        {
            RegisterHotKey(hwnd, id, modifiers, key);
        }

        /// <summary>
        /// the main delegate for unregistering an !!ALREADY REGISTERED!! HOTKEY
        /// </summary>
        /// <param name="hwnd">the HotKey KeyPress Handler</param>
        /// <param name="id">the HotKey ID</param>
        private static void UnRegisterHotKeyInternal(IntPtr hwnd, int id)
        {
            UnregisterHotKey(_hwnd, id);
        }

        /// <summary>
        /// the KeyPress event binding for the KeyEvent
        /// </summary>
        /// <param name="e">the HotKey Event Args</param>
        private static void OnHotKeyPressed(HotKeyEventArgs e)
        {
            //if the main HotKeyManager has a HotKey event 
            if (HotKeyManager.HotKeyPressed != null)
            {
                //we remove it
                HotKeyManager.HotKeyPressed(null, e);
            }
        }

        /// <summary>
        /// the initial MessageWindow (volatile for delegate work)
        /// </summary>
        private static volatile MessageWindow _wnd;
        /// <summary>
        /// the main pointer for the Handler (volatile for use within delegates) 
        /// </summary>
        private static volatile IntPtr _hwnd;
        /// <summary>
        /// the Reset Event for Manual Reset on the Event
        /// </summary>
        private static ManualResetEvent _windowReadyEvent = new ManualResetEvent(false);
        /// <summary>
        /// the HotKey Manager for binding use
        /// </summary>
        static HotKeyManager()
        {
            //the main MessageThread
            Thread messageLoop = new Thread(delegate ()
            {
                //we loop through the windows
                Application.Run(new MessageWindow());
            });
            //the the name to the thread loop
            messageLoop.Name = "MessageLoopThread";
            //and place it in the background
            messageLoop.IsBackground = true;
            //before starting the loop
            messageLoop.Start();
        }

        /// <summary>
        /// the main MessageWindow Form Class
        /// </summary>
        private class MessageWindow : Form
        {
            /// <summary>
            /// the initial Message Initializer
            /// </summary>
            public MessageWindow()
            {
                //we initialize the window Procedure Elements
                _wnd = this;
                _hwnd = this.Handle;
                _windowReadyEvent.Set();
            }
            /// <summary>
            /// the initial ovverride event for the HotKey Binding
            /// </summary>
            /// <param name="m">the message for error references</param>
            protected override void WndProc(ref System.Windows.Forms.Message m)
            {
                //if the Msg is the passed HotKey
                if (m.Msg == WM_HOTKEY)
                {
                    //the main HotKey Event Args
                    HotKeyEventArgs e = new HotKeyEventArgs(m.LParam);
                    HotKeyManager.OnHotKeyPressed(e);
                }

                base.WndProc(ref m);
            }

            protected override void SetVisibleCore(bool value)
            {
                // Ensure the window never becomes visible
                base.SetVisibleCore(false);
            }

            //the initial KeyPress for the HotKey
            private const int WM_HOTKEY = 0x312;
        }

        #region Dll Imports
        [DllImport("user32", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32", SetLastError = true)]
        #endregion
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static int _id = 0;
    }

    /// <summary>
    /// the main Hot Key EventArgs
    /// </summary>
    public class HotKeyEventArgs : EventArgs
    {
        //the initial Key
        public readonly Keys Key;
        //
        public readonly KeyModifiers Modifiers;

        public HotKeyEventArgs(Keys key, KeyModifiers modifiers)
        {
            this.Key = key;
            this.Modifiers = modifiers;
        }

        public HotKeyEventArgs(IntPtr hotKeyParam)
        {
            uint param = (uint)hotKeyParam.ToInt64();
            Key = (Keys)((param & 0xffff0000) >> 16);
            Modifiers = (KeyModifiers)(param & 0x0000ffff);
        }
    }

    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        NoRepeat = 0x4000,
        None = (Int32)Keys.None //[NONE]
    }
}
