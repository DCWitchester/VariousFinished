#pragma checksum "D:\WebSite\BlazorApp2\Pages\Counter.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f65d83b382654760571e925b8a55a31fd1af8dc1"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorApp2.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\WebSite\BlazorApp2\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\WebSite\BlazorApp2\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\WebSite\BlazorApp2\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\WebSite\BlazorApp2\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\WebSite\BlazorApp2\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\WebSite\BlazorApp2\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\WebSite\BlazorApp2\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\WebSite\BlazorApp2\_Imports.razor"
using BlazorApp2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\WebSite\BlazorApp2\_Imports.razor"
using BlazorApp2.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
using BlazorApp2.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
using System.Text;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
using System.Xml.Serialization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/counter")]
    public partial class Counter : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Selectie Produse</h1>\r\n<br>\r\n");
            __builder.OpenElement(1, "table");
            __builder.AddAttribute(2, "class", "table");
            __builder.AddMarkupContent(3, "\r\n    ");
            __builder.AddMarkupContent(4, "<thead>\r\n        <tr>\r\n            <th>Cod Produs</th>\r\n            <th>Denumire</th>\r\n            <th>Selectie</th>\r\n        </tr>\r\n    </thead>\r\n    ");
            __builder.OpenElement(5, "tbody");
            __builder.AddMarkupContent(6, "\r\n");
#nullable restore
#line 22 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
         foreach (var produs in products.productList)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(7, "            ");
            __builder.OpenElement(8, "tr");
            __builder.AddMarkupContent(9, "\r\n                ");
            __builder.OpenElement(10, "td");
            __builder.AddContent(11, 
#nullable restore
#line 25 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
                     produs.codProdus

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\r\n                ");
            __builder.OpenElement(13, "td");
            __builder.AddContent(14, 
#nullable restore
#line 26 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
                     produs.denumireProdus

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n                ");
            __builder.OpenElement(16, "td");
            __builder.AddMarkupContent(17, "\r\n                    ");
            __builder.OpenElement(18, "button");
            __builder.AddAttribute(19, "class", "btn" + " " + (
#nullable restore
#line 28 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
                                        produs.btnType

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(20, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 28 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
                                                                    e => IncrementCount(e,produs.codProdus)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(21, "\r\n                        Selecteaza\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n");
#nullable restore
#line 33 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(25, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n    ");
            __builder.OpenElement(27, "button");
            __builder.AddAttribute(28, "class", "btn btn-primary");
            __builder.AddAttribute(29, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 35 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
                                              setVotingProducts

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(30, "Finalizeaza selectia");
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n    ");
            __builder.OpenElement(32, "button");
            __builder.AddAttribute(33, "class", "btn btn-primary");
            __builder.AddAttribute(34, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 36 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
                                              resetVotingProducts

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(35, "Reseteaza selectia");
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 40 "D:\WebSite\BlazorApp2\Pages\Counter.razor"
       
    private String xmlDocument = String.Empty;
    private XmlSerializer serializer = new XmlSerializer(typeof(Retete));
    private Structures.Products products = new Structures.Products();

    private void IncrementCount(MouseEventArgs e, String codp)
    {
        Structures.Product product = products.productList.FirstOrDefault(x => x.codProdus == codp);
        if (!product.Equals(null))
        {
            if (product.voted)
            {
                product.voted = false;
                product.btnType = "btn-primary";
            }
            else
            {
                product.voted = true;
                product.btnType = "btn-secondary";
            }
        }
    }
    private async void setVotingProducts()
    {
        await http.GetAsync(WebMethods.SetResetVoteCount);
        String selectedProducts = String.Empty;
        foreach (String codp in products.productList.Where(x => x.voted).Select(x => x.codProdus))
        {
            if (String.IsNullOrWhiteSpace(selectedProducts)) selectedProducts += "%27" + codp.Trim() + "%27";
            else selectedProducts += ",%27" + codp.Trim() + "%27";
        }
        if (!String.IsNullOrWhiteSpace(selectedProducts))
        {
            await http.GetAsync(WebMethods.SetVotingProducts + selectedProducts);
            myNavigationManager.NavigateTo("/final");
        }
    }
    private async void resetVotingProducts()
    {
        await http.GetStringAsync(WebMethods.SetResetVotingProducts);
    }
    private void deserializeDocument(String xmlDocument)
    {
        Retete retete = new Retete();
        using (TextReader reader = new StringReader(xmlDocument))
        {
            retete = (Retete)serializer.Deserialize(reader);
        }
        products.initializeListFromRetete(retete);
    }
    protected override async Task OnInitializedAsync()
    {
        xmlDocument = await http.GetStringAsync(WebMethods.GetRetete);
        await Task.Run(() => deserializeDocument(xmlDocument));
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager myNavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591