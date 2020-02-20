using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSynch.Miscellaneous
{
    /// <summary>
    /// this class will contain all functions that have no other place in the world
    /// </summary>
    class Miscellaneous
    {
        //All non-Auxilliary functions in this class are to be static and public
        /// <summary>
        /// this function will close the current programs execution
        /// </summary>
        public static void ProgramClose()
        {
            Process.GetCurrentProcess().Kill();
        }
        /// <summary>
        /// this function will be used to move a window passed as parameter
        /// </summary>
        /// <param name="e">the Mouse Input needed for moving the form</param>
        /// <param name="window">the given window</param>
        public static void MoveWindow(System.Windows.Input.MouseButtonEventArgs e, System.Windows.Window window)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                window.DragMove();
        }
    }
}
