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
    }
}
