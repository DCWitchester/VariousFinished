#pragma checksum "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b50ad7bc7424a068a20105968142f2bbeb90e41"
// <auto-generated/>
#pragma warning disable 1591
namespace ProductDisplay.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using ProductDisplay;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\_Imports.razor"
using ProductDisplay.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Main : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<style>\r\n    body {\r\n        background: linear-gradient(90deg, rgb(5, 39, 103) 0%, #3a0647 70%);\r\n        font-size: large;\r\n    }\r\n</style>\r\n\r\n");
#nullable restore
#line 13 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
 if (!HasProductBeenScanned)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(1, "    ");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "bg-centerContent");
            __builder.AddMarkupContent(4, "\r\n        ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "bg-mentorStyle");
            __builder.AddMarkupContent(7, "\r\n            ");
            __builder.AddMarkupContent(8, "<h1 style=\"margin:5px\">Va rog scanati produsul</h1>\r\n            ");
            __builder.OpenElement(9, "input");
            __builder.AddAttribute(10, "type", "text");
            __builder.AddAttribute(11, "id", "ProductCode");
            __builder.AddAttribute(12, "name", "ProductCode");
            __builder.AddAttribute(13, "style", "margin-bottom:5px");
            __builder.AddAttribute(14, "maxlength", "12");
            __builder.AddAttribute(15, "onkeypress", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 18 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
                                                                                                                                                                      ValidForm

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(16, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 18 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
                                                                                                                         productController.ProductCode

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => productController.ProductCode = __value, productController.ProductCode));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n            <br>\r\n            ");
            __builder.OpenElement(19, "label");
            __builder.AddAttribute(20, "style", "color:red;" + " margin:5px;" + " font-size:large;" + " visibility:" + (
#nullable restore
#line 20 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
                                                                              errorVisibility

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(21, "Cod de produs inexistent");
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n");
#nullable restore
#line 23 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(25, "    ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "bg-centerContent");
            __builder.AddMarkupContent(28, "\r\n        ");
            __builder.OpenElement(29, "div");
            __builder.AddAttribute(30, "class", "bg-mentorStyle");
            __builder.AddMarkupContent(31, "\r\n            ");
            __builder.OpenElement(32, "div");
            __builder.AddAttribute(33, "style", "width:100%; margin-left:25px; margin-right:25px; display:flex; font-size:large");
            __builder.OpenElement(34, "h1");
            __builder.AddContent(35, 
#nullable restore
#line 28 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
                                                                                                             productController.ProductName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n            ");
            __builder.OpenElement(37, "div");
            __builder.AddAttribute(38, "style", "display:flex; border-top: 1px solid black; border-bottom:1px solid black");
            __builder.AddMarkupContent(39, "\r\n                ");
            __builder.AddMarkupContent(40, "<div style=\"width:50%; float:left; font-size:large\"><h2>Pret:</h2></div>\r\n                ");
            __builder.OpenElement(41, "div");
            __builder.AddAttribute(42, "style", "width:50%; float:right; border-left: 1px solid black; font-size:large");
            __builder.OpenElement(43, "h2");
            __builder.AddContent(44, 
#nullable restore
#line 31 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
                                                                                                        productController.ProductPrice

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(45, " Lei");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(46, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(47, "\r\n            ");
            __builder.OpenElement(48, "button");
            __builder.AddAttribute(49, "style", "margin-top:10px");
            __builder.AddAttribute(50, "class", "btn-primary");
            __builder.AddAttribute(51, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 33 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"
                                                                          ResetPage

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(52, "Inapoi");
            __builder.CloseElement();
            __builder.AddMarkupContent(53, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(54, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(55, "\r\n");
#nullable restore
#line 36 "D:\AlfaBeta\VizualizarePreturi\Aplicatie\ProductDisplay\Pages\Main.razor"

}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
