using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BarcodeScanner.Services;
using ZXing.Mobile;
using Xamarin.Forms;

[assembly : Dependency(typeof(BarcodeScanner.Droid.Services.BarcodeScanningService))]

namespace BarcodeScanner.Droid.Services
{
    public class BarcodeScanningService : IBarcodeScanningService
    {
        public async Task<String> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}