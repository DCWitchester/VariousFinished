#pragma checksum "D:\PosTable\POSTable\Pages\TableMenu - Copy.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d591d01cebd63d7447b4bfae4ea5cd4597392f1d"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace POSTable.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\PosTable\POSTable\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PosTable\POSTable\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\PosTable\POSTable\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\PosTable\POSTable\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\PosTable\POSTable\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\PosTable\POSTable\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\PosTable\POSTable\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\PosTable\POSTable\_Imports.razor"
using POSTable;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\PosTable\POSTable\_Imports.razor"
using POSTable.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\PosTable\POSTable\Pages\TableMenu - Copy.razor"
using POSTable.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\PosTable\POSTable\Pages\TableMenu - Copy.razor"
using System.Xml.Serialization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\PosTable\POSTable\Pages\TableMenu - Copy.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\PosTable\POSTable\Pages\TableMenu - Copy.razor"
using System.Net;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/tablemenu")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/tablemenu/{table:int}")]
    public partial class TableMenu___Copy : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 58 "D:\PosTable\POSTable\Pages\TableMenu - Copy.razor"
       
    [Parameter]
    public Int32 table { get; set; }

    private XmlSerializer serializer = new XmlSerializer(typeof(XmlClasses.Meniu));
    private String xmlDocument = String.Empty;

    private ObjectStructures.Menu menu = new ObjectStructures.Menu();
    private ObjectStructures.Menu sale = new ObjectStructures.Menu();

    protected override async Task OnInitializedAsync()
    {
        //we retrieve the votable List from the webService
        xmlDocument = await http.GetStringAsync(WebMethods.GetMenu);
        await Task.Run(() => deserializeDocument(xmlDocument));
    }

    private void deserializeDocument(String xmlDocument)
    {
        XmlClasses.Meniu meniu = new XmlClasses.Meniu();
        using (TextReader reader = new StringReader(xmlDocument))
        {
            meniu = (XmlClasses.Meniu)serializer.Deserialize(reader);
        }
        menu.InitializeMenuFromServer(meniu);
    }
    private void SelectItem(MouseEventArgs e, Int32 codp)
    {
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x=>x.ProductCode==codp);
        if (!item.Equals(null))
        {
            if (!item.IsSelected)
            {
                item.IsSelected = !item.IsSelected;
                item.ProductQuantity = 1;
            }
            else if (item.ProductQuantity == 0) item.IsSelected = false;
        }

    }
    private void ResetSelection()
    {
        foreach(var item in menu.itemList)
        {
            item.IsSelected = false;
            item.ProductQuantity = 0;
        }
    }
    private void AddQuantity(MouseEventArgs e, Int32 codp)
    {
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x=>x.ProductCode==codp);
        item.ProductQuantity++;
    }
    private void SubtractQuantity(MouseEventArgs e, Int32 codp)
    {
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x=>x.ProductCode==codp);
        if(item.ProductQuantity>0) item.ProductQuantity--;

    }

    private async void CreateSales()
    {
        sale.itemList = menu.itemList.Where(x => x.ProductQuantity > 0).ToList();
        sale.SetSaleTime();
        sale.Table = table;
        String document = sale.CreateSale();
        await http.GetAsync(WebMethods.SetSales + document);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager myNavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591