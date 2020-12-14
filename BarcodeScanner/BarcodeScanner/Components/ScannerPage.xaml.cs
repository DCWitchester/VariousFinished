using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace BarcodeScanner.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageScanner : ContentPage
    {
        #region LocalParameters
#pragma warning disable IDE1006 // Naming Styles
        Backbone.BarcodeScannerController instanceController { get; set; }
#pragma warning restore IDE1006 // Naming Styles
        #endregion

        #region Constructors
        public PageScanner()
        {
            InitializeComponent();
        }

        public PageScanner(Backbone.BarcodeScannerController controller)
        {
            instanceController = controller;
            InitializeComponent();
        }
        #endregion

        #region Button Events

#pragma warning disable IDE1006 // Naming Styles
        public void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>  await GetQuantity(result));
        }
#pragma warning restore IDE1006 // Naming Styles

        public async Task GetQuantity(Result result)
        {
            String quantity = await DisplayPromptAsync("Cantitate", "Introduceti cantitatea:", keyboard: Keyboard.Numeric);
            instanceController.Products.Add(new ObjectClasses.Products
            {
                ID = instanceController.Products.Count + 1,
                ProductName = Services.WebServiceMethods.GetProductName(result.Text),
                ProductCode = result.Text,
                ProductQuantity = quantity
            });
            await instanceController.PageNavigation.PopAsync(true);
        }

        private void ReturnToMainPage(object sender, EventArgs e)
        {
            instanceController.PageNavigation.PopAsync(true);
        }

        #endregion


    }
}