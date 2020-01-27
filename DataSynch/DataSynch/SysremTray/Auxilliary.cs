using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSynch.SysTray
{
    class Auxilliary
    {
        void GenerateInstance()
        {
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString(), out Boolean createdNew))
            {
                if (!createdNew)
                {
                    Miscellaneous.Miscellaneous.ProgramClose();
                }
            }
        }
    }
}
