//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("BarcodeScanner.Components.ScannerPage.xaml", "Components/ScannerPage.xaml", typeof(global::BarcodeScanner.Components.PageScanner))]

namespace BarcodeScanner.Components {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Components\\ScannerPage.xaml")]
    public partial class PageScanner : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::ZXing.Net.Mobile.Forms.ZXingScannerView scanView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(PageScanner));
            scanView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::ZXing.Net.Mobile.Forms.ZXingScannerView>(this, "scanView");
        }
    }
}
