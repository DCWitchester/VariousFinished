using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BarcodeScanner.Services;
using ZXing;

namespace BarcodeScanner
{
    public partial class MainPage : ContentPage
    {

#pragma warning disable IDE1006 // Naming Styles
        Backbone.BarcodeScannerController instanceController { get; set; }
#pragma warning restore IDE1006 // Naming Styles


        public MainPage()
        {

            InitializeComponent();
            instanceController = new Backbone.BarcodeScannerController(this.Navigation);
            productsListView.ItemsSource = instanceController.Products;
        }

        private async void CallScanPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Components.PageScanner(instanceController),true);
        }

        private async void SendProductItems(object sender,EventArgs e)
        {
            WebServiceMethods.SendProductQunatitites(instanceController.Products.ToList());

            instanceController.Products = new System.Collections.ObjectModel.ObservableCollection<ObjectClasses.Products>();
        }


    }
}
