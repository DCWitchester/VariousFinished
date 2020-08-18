#pragma checksum "D:\PosTable\POSTable\Pages\TableMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5463a5c807882da539fb033942fc662fac414a07"
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
#line 8 "D:\PosTable\POSTable\Pages\TableMenu.razor"
using POSTable.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\PosTable\POSTable\Pages\TableMenu.razor"
using System.Xml.Serialization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\PosTable\POSTable\Pages\TableMenu.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\PosTable\POSTable\Pages\TableMenu.razor"
using System.Net;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/tablemenu")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/tablemenu/{table:int}")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/tablemenu/{table:int}/{tableCheck:bool}")]
    public partial class TableMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 229 "D:\PosTable\POSTable\Pages\TableMenu.razor"
       
    //The initialization of the page parameters
    /// <summary>
    /// the Table parameter is used to link the sale to the table within the POS Structure
    /// </summary>
    [Parameter]
    public Int32 table { get; set; }
    /// <summary>
    /// the TableCheck parameter is used for checking an open
    /// </summary>
    [Parameter]
    public Boolean tableCheck { get; set; }

    public Boolean checkSale { get; set; } = false;
    public Boolean validateSale { get; set; } = false;
    public String checkSaleDisplay { get; set; } = "Vizualizare Comanda";

    /// <summary>
    /// the main enum is used for setting the quantity events: adding or subtracting one
    /// </summary>
    private enum QuantityEvents
    {
        add = 0,
        substract = 1
    }

    /// <summary>
    /// the menu structure used for displaying the items
    /// </summary>
    private ObjectStructures.Menu menu = new ObjectStructures.Menu();
    /// <summary>
    ///the main sale structure used for calling the web Service
    /// </summary>
    private ObjectStructures.Menu sale = new ObjectStructures.Menu();

    protected override async Task OnInitializedAsync()
    {
        //we initialize a string for containing the retrieved xmlDocuments
        String xmlDocument = String.Empty;
        //first we check if the table is open so we call the WebMethod with the table code
        xmlDocument = await http.GetStringAsync(WebMethods.GetIsTableOpen + table.ToString());
        //we deserialize the object and read the answe
        Boolean answer = await Task.Run(() => CheckTable(xmlDocument));
        //if the tableCheck has not been made prior and the table has an active sale we display a message that the table is open
        if (answer && !tableCheck) myNavigationManager.NavigateTo("/tableopen/" + table);
        else
        {
            String xmlDocumentAdministrations = await http.GetStringAsync(WebMethods.GetAdministrations);
            String xmlDocumentCategories = await http.GetStringAsync(WebMethods.GetCategories);
            //we retrieve the votable List from the webService
            xmlDocument = await http.GetStringAsync(WebMethods.GetMenu);
            //and deserialize the object to the needed structure
            await Task.Run(() => DeserializeDocument(xmlDocument))
                        .ContinueWith((t) => DeserializeCategories(xmlDocumentAdministrations, xmlDocumentCategories));
        }
    }

    /// <summary>
    /// this function will deserialize the xml Menu Document
    /// </summary>
    /// <param name="xmlDocument">the retrieved XmlDocument</param>
    private void DeserializeDocument(String xmlDocument)
    {
        //we initialize a serializer over the menu object
        XmlSerializer serializer = new XmlSerializer(typeof(XmlClasses.Meniu));
        //initialize a new Meniu class
        XmlClasses.Meniu meniu = new XmlClasses.Meniu();
        //then using a text reader
        using (TextReader reader = new StringReader(xmlDocument))
        {
            //we deserialize the meniu to a class
            meniu = (XmlClasses.Meniu)serializer.Deserialize(reader);
        }
        //and initialize the global menu from the deserialized object
        menu.InitializeMenuFromServer(meniu);
    }

    private void DeserializeCategories(String xmlDocumentAdministrations, String xmlDocumentCategories)
    {
        XmlSerializer administrationsSerializer = new XmlSerializer(typeof(XmlClasses.Administrations));
        XmlSerializer categorySerializer = new XmlSerializer(typeof(XmlClasses.Categories));
        XmlClasses.Administrations administrations = new XmlClasses.Administrations();
        XmlClasses.Categories categories = new XmlClasses.Categories();
        using (TextReader reader = new StringReader(xmlDocumentAdministrations))
        {
            administrations = (XmlClasses.Administrations)administrationsSerializer.Deserialize(reader);
        }
        using (TextReader reader1 = new StringReader(xmlDocumentCategories))
        {
            categories = (XmlClasses.Categories)categorySerializer.Deserialize(reader1);
        }
        menu.InitializeMenuCategoriesFromServer(categories, administrations);
    }

    /// <summary>
    /// the function will deserialize the answer xmlDocument to check if the table is Open
    /// </summary>
    /// <param name="xmlDocument">the XmlDocument retrieve from the WebService</param>
    /// <returns>the answer value</returns>
    private Boolean CheckTable(String xmlDocument)
    {
        //we initialize a new answe object
        XmlClasses.Answer answer = new XmlClasses.Answer();
        //and a new serializer over the answer object
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlClasses.Answer));
        //then using a text reader
        using (TextReader reader = new StringReader(xmlDocument))
        {
            //we deserialize the object
            answer = (XmlClasses.Answer)xmlSerializer.Deserialize(reader);
        }
        //and return the answer value
        return answer.Valoare;
    }

    /// <summary>
    /// the main function for the initial select of an item
    /// </summary>
    /// <param name="e">the click on the main div item</param>
    /// <param name="codp">the current items product code</param>
    private void SelectItem(MouseEventArgs e, Int32 codp)
    {
        //we retrieve the object from the list
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x => x.ProductCode == codp);
        //if no object was selected
        //chances for this to happen are slim to none but we will still check
        if (!item.Equals(null))
        {
            //if the item wasn't selected prior
            if (!item.IsSelected)
            {
                //we select it
                item.IsSelected = true;
                //and set the quantity to one
                item.ProductQuantity = 1;
            }
            else if (item.ProductQuantity == 1)
            {
                //if it was selected and the quantity is one
                //we deselect the item
                item.IsSelected = false;
                //and set the quantity to 0
                item.ProductQuantity = 0;
            }
        }

    }

    /// <summary>
    /// this function will reset the selection for all the items
    /// </summary>
    private void ResetSelection()
    {
        //we iterate the list
        foreach (var item in menu.itemList)
        {
            //and deselect the items
            item.IsSelected = false;
            //and set the quantity to 0
            item.ProductQuantity = 0;
        }
    }

    /// <summary>
    /// this function will increase the quantity by 1
    /// deprecated
    /// </summary>
    /// <param name="e">the click event</param>
    /// <param name="codp">the current ProductCode</param>
    private void AddQuantity(MouseEventArgs e, Int32 codp)
    {
        //we retrieve the item from the list
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x => x.ProductCode == codp);
        //we increase the quantity by one
        item.ProductQuantity++;
    }

    /// <summary>
    /// this function will decrease the quantity by 1
    /// deprecated
    /// </summary>
    /// <param name="e">the click event</param>
    /// <param name="codp">the current ProductCode</param>
    private void SubtractQuantity(MouseEventArgs e, Int32 codp)
    {
        //we retrieve the item from the list
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x => x.ProductCode == codp);
        //we decrease the quantity by one
        if (item.ProductQuantity > 0) item.ProductQuantity--;
    }

    /// <summary>
    /// the new function for altering the quantity
    /// replaces AddQunatity and SubstractQuantity
    /// </summary>
    /// <param name="e">the click event</param>
    /// <param name="codp">the Product Code</param>
    /// <param name="events">the quantity event</param>
    private void AlterQuantity(MouseEventArgs e, Int32 codp, QuantityEvents events)
    {
        //we will retrieve the needed product
        ObjectStructures.MenuItem item = menu.itemList.FirstOrDefault(x => x.ProductCode == codp);
        //we check the event and either increase or decrease the item
        if (events == QuantityEvents.add) item.ProductQuantity++;
        else if (events == QuantityEvents.substract && item.ProductQuantity > 0) item.ProductQuantity--;
        //and if the product quantity is 0 we deselect the product
        if (item.ProductQuantity == 0) item.IsSelected = false;
    }

    /// <summary>
    /// the main release function
    /// </summary>
    private async void CreateSales()
    {
        //we retrieve the items with positive quantity
        sale.itemList = menu.itemList.Where(x => x.ProductQuantity > 0).ToList();
        //then we set the sale time
        //this is no longer used
        sale.SetSaleTime();
        //we set the tableto the sale
        sale.Table = table;
        //then we serialize the sale item into a JSON Object
        String document = sale.CreateSale();
        //we will then call the web Method
        await http.GetAsync(WebMethods.SetSales + document);
        //then we will navigate the page
        myNavigationManager.NavigateTo("/endmessage");
    }

    private void DisplayCategory(MouseEventArgs e, Int32 categoryCode)
    {
        ObjectStructures.MenuCategory menuCategory = menu.menuCategories.Where(x => x.CategoryCode == categoryCode).FirstOrDefault();
        menuCategory.IsOpened = !menuCategory.IsOpened;
    }

    private void CheckSale()
    {
        checkSale = !checkSale;
        if (!checkSale) checkSaleDisplay = "Vizualizare Comanda";
        else checkSaleDisplay = "Inapoi la selectie";
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager myNavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient http { get; set; }
    }
}
#pragma warning restore 1591
