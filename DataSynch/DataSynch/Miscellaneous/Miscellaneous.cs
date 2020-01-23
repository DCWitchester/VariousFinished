using System.Diagnostics;

namespace DataSynch.Miscellaneous
{
    /// <summary>
    /// this class contains all special functions that have no place anywhere else
    /// </summary>
    class Miscellaneous
    {
        /// <summary>
        /// the main function for closing the program
        /// </summary>
        public void ProgramClose()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
