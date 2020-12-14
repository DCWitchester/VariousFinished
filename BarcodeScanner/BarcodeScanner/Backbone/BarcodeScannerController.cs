using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BarcodeScanner.Backbone
{
    public class BarcodeScannerController
    {
        #region Properties
#pragma warning disable IDE1006 // Naming Styles
        protected INavigation pageNavigation { get; set; }

        protected ObjectClasses.Barcode barcode { get; set; }

        protected ObservableCollection<ObjectClasses.Products> products { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        #endregion

        #region Callers 
        public INavigation PageNavigation
        {
            get => pageNavigation;
            set => pageNavigation = value;
        }

        public ObjectClasses.Barcode Barcode 
        {
            get => barcode;
            set => barcode = value;
        }

        public ObservableCollection<ObjectClasses.Products> Products
        {
            get => products;
            set => products = value;
        }
        #endregion

        #region Constructors
        public BarcodeScannerController() 
        {
            barcode = new ObjectClasses.Barcode();
            products = new ObservableCollection<ObjectClasses.Products>();
        }

        public BarcodeScannerController(INavigation navigation)
        {
            barcode = new ObjectClasses.Barcode();
            products = new ObservableCollection<ObjectClasses.Products>();
            pageNavigation = navigation;
        }
        #endregion

    }
}
