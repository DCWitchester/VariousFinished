using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using ProductDisplay.Classes;
using System;
using System.Threading.Tasks;



namespace ProductDisplay.Pages
{
    public partial class Main
    {
        //the onAfterRenderAsync is raised after every form refresh
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!HasProductBeenScanned)
            {
                //we can't access the base html objects from c# so we need JavaScripts(Damn the elders of the Internet)
                await JSRuntime.InvokeVoidAsync("focusElement", "ProductCode");
                //the call the StateHasChanged to force a page refresh
                this.StateHasChanged();
            }
        }

        /// <summary>
        /// the settings for the pageState
        /// </summary>
        private Boolean HasProductBeenScanned { get; set; } = false;

        /// <summary>
        /// the main product Controller for the page
        /// </summary>
        private ProductController productController { get; set; } = new ProductController();

        //the main validation event on the form raised by each and every key press
        async void ValidForm(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await Task.Run(() => UpdateProductController());
            }
        }

        private async void UpdateProductController()
        {
            String xmlDocument;
            try
            {
                xmlDocument = await http.GetStringAsync(ProductDisplay.WebFunctions.WebMehtods.GetProduct +
                                                            productController.ProductCode);
            }catch { return; }
            System.Xml.Serialization.XmlSerializer serializer
                = new System.Xml.Serialization.XmlSerializer(typeof(ProductDisplay.WebFunctions.XmlObjects.ProductDisplay));
            ProductDisplay.WebFunctions.XmlObjects.ProductDisplay productDisplay = new WebFunctions.XmlObjects.ProductDisplay();
            using (System.IO.TextReader reader = new System.IO.StringReader(xmlDocument))
            {
                productDisplay = (ProductDisplay.WebFunctions.XmlObjects.ProductDisplay)serializer.Deserialize(reader);
            }
            productDisplay.SetProductDisplayToObject(productController);
            ChangePageState();
        }

        async void ChangePageState()
        {
            HasProductBeenScanned = !HasProductBeenScanned;
            if (HasProductBeenScanned) { }
            else { }
        }

        void ResetPage()
        {
            productController = new ProductController();
            ChangePageState();
        }
    }
}
