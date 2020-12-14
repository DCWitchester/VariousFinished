using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner.Services
{
    public interface IBarcodeScanningService
    { 
       Task<string> ScanAsync();
    }
}
