using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WorkOrder
{
    class Miscellaneous
    {
        #region Control Miscellaneous
        /// <summary>
        /// this function will close the current instance of the program, I HOPE
        /// </summary>
        public static void ProgramClose()
        {
            Process.GetCurrentProcess().Kill();
        }
        #endregion
    }
}
