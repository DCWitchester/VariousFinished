#pragma checksum "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b2c2d2edba7e0fe89568f785070818590ce61c5"
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
#line 3 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
using BlazorApp2.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
using System.Text;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
using System.Xml.Serialization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/votecount")]
    public partial class VoteCounts : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Vot Produse</h1>\r\n<br>\r\n");
            __builder.OpenElement(1, "table");
            __builder.AddAttribute(2, "class", "table");
            __builder.AddMarkupContent(3, "\r\n    ");
            __builder.AddMarkupContent(4, "<thead>\r\n        <tr>\r\n            <th>Cod Produs</th>\r\n            <th>Denumire</th>\r\n            <th>Selectie</th>\r\n        </tr>\r\n    </thead>\r\n    ");
            __builder.OpenElement(5, "tbody");
            __builder.AddMarkupContent(6, "\r\n");
#nullable restore
#line 22 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
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
#line 25 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
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
#line 26 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
                     produs.denumireProdus

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n                ");
            __builder.OpenElement(16, "td");
            __builder.AddContent(17, 
#nullable restore
#line 27 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
                     produs.votedCount

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n");
#nullable restore
#line 29 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(20, "    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 34 "D:\WebSite\BlazorApp2\Pages\VoteCounts.razor"
       
    private String xmlDocument = String.Empty;
    private XmlSerializer serializer = new XmlSerializer(typeof(Counts));
    private Structures.Products products = new Structures.Products();

    private void deserializeDocument(String xmlDocument)
    {
        Counts counts = new Counts();
        using (TextReader reader = new StringReader(xmlDocument))
        {
            counts = (Counts)serializer.Deserialize(reader);
        }
        products.initializeListFromVoteCount(counts);
    }

    protected override async Task OnInitializedAsync()
    {
        xmlDocument = await http.GetStringAsync(WebMethods.GetVotesForRetete);
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
