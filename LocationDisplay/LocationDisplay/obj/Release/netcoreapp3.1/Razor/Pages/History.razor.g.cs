#pragma checksum "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4ef64e07414b5ba339459ed2bdfc27a4c1d423bc"
// <auto-generated/>
#pragma warning disable 1591
namespace LocationDisplay.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using LocationDisplay;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\locationDisplay\LocationDisplay\LocationDisplay\_Imports.razor"
using LocationDisplay.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
using GoogleMapsComponents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
using GoogleMapsComponents.Maps;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/history")]
    public partial class History : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Istoric Agent</h1>\r\n\r\n");
#nullable restore
#line 11 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
 if (locations == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(1, "    ");
            __builder.AddMarkupContent(2, "<p><em>Loading...</em></p>\r\n");
#nullable restore
#line 14 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
}
else
{


#line default
#line hidden
#nullable disable
            __builder.AddContent(3, "    ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "style", " background-color: #80aaff; border : solid black 1px; margin-bottom: 2px; display: flex ");
            __builder.AddMarkupContent(6, "\r\n        ");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(7);
            __builder.AddAttribute(8, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 19 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                         historyController

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(9, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(10, " \r\n            ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(11);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(12, "\r\n            ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.ValidationSummary>(13);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(14, "\r\n            ");
                __builder2.OpenElement(15, "div");
                __builder2.AddMarkupContent(16, "\r\n                ");
                __builder2.OpenElement(17, "p");
                __builder2.AddMarkupContent(18, "\r\n                    ");
                __builder2.OpenElement(19, "div");
                __builder2.AddAttribute(20, "style", "margin-top:10px; margin-left:10px");
                __builder2.AddAttribute(21, "s", true);
                __builder2.AddMarkupContent(22, "\r\n                        ");
                __builder2.OpenElement(23, "label");
                __builder2.AddMarkupContent(24, "\r\n                            Coduri Agent:\r\n                            ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(25);
                __builder2.AddAttribute(26, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 27 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                    historyController.AgentFilter

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(27, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => historyController.AgentFilter = __value, historyController.AgentFilter))));
                __builder2.AddAttribute(28, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => historyController.AgentFilter));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(29, "\r\n                        ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(30, "\r\n                    ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(31, "\r\n                ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(32, "\r\n                ");
                __builder2.AddMarkupContent(33, "<h5 style=\"margin-left:10px\">Perioda afisata</h5>\r\n                ");
                __builder2.OpenElement(34, "p");
                __builder2.AddMarkupContent(35, "\r\n                    ");
                __builder2.OpenElement(36, "div");
                __builder2.AddAttribute(37, "style", "margin-left:10px");
                __builder2.AddMarkupContent(38, "\r\n                        ");
                __builder2.OpenElement(39, "label");
                __builder2.AddAttribute(40, "style", "margin-right:2px");
                __builder2.AddMarkupContent(41, "\r\n                            Moment initial:\r\n                            ");
                __builder2.OpenElement(42, "input");
                __builder2.AddAttribute(43, "type", "date");
                __builder2.AddAttribute(44, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 36 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                              historyController.initialDateTime.date

#line default
#line hidden
#nullable disable
                , format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.AddAttribute(45, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => historyController.initialDateTime.date = __value, historyController.initialDateTime.date, format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(46, "\r\n                            ");
                __builder2.OpenElement(47, "input");
                __builder2.AddAttribute(48, "type", "time");
                __builder2.AddAttribute(49, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 37 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                              historyController.initialDateTime.time

#line default
#line hidden
#nullable disable
                , format: "HH:mm:ss", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.AddAttribute(50, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => historyController.initialDateTime.time = __value, historyController.initialDateTime.time, format: "HH:mm:ss", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(51, "\r\n                        ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(52, "\r\n                        ");
                __builder2.OpenElement(53, "label");
                __builder2.AddAttribute(54, "style", "margin-right:2px");
                __builder2.AddMarkupContent(55, "\r\n                            Moment final:\r\n                            ");
                __builder2.OpenElement(56, "input");
                __builder2.AddAttribute(57, "type", "date");
                __builder2.AddAttribute(58, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 41 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                              historyController.finalDateTime.date

#line default
#line hidden
#nullable disable
                , format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.AddAttribute(59, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => historyController.finalDateTime.date = __value, historyController.finalDateTime.date, format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(60, "\r\n                            ");
                __builder2.OpenElement(61, "input");
                __builder2.AddAttribute(62, "type", "time");
                __builder2.AddAttribute(63, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 42 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                              historyController.finalDateTime.time

#line default
#line hidden
#nullable disable
                , format: "HH:mm:ss", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.AddAttribute(64, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => historyController.finalDateTime.time = __value, historyController.finalDateTime.time, format: "HH:mm:ss", culture: global::System.Globalization.CultureInfo.InvariantCulture));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(65, "\r\n                        ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(66, "\r\n                    ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(67, "\r\n                ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(68, "\r\n                ");
                __builder2.OpenElement(69, "p");
                __builder2.AddMarkupContent(70, "\r\n                    ");
                __builder2.OpenElement(71, "div");
                __builder2.AddAttribute(72, "style", "margin-left:10px");
                __builder2.AddMarkupContent(73, "\r\n                        ");
                __builder2.OpenElement(74, "button");
                __builder2.AddAttribute(75, "id", "btnStaticHistory");
                __builder2.AddAttribute(76, "type", "submit");
                __builder2.AddAttribute(77, "class", "btn-primary");
                __builder2.AddAttribute(78, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 48 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                                                                  ValidateFilter

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddContent(79, "Afiseaza Istoric");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(80, "\r\n                        ");
                __builder2.OpenElement(81, "button");
                __builder2.AddAttribute(82, "id", "btnAnimatedHistory");
                __builder2.AddAttribute(83, "type", "submit");
                __builder2.AddAttribute(84, "class", "btn-primary");
                __builder2.AddAttribute(85, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 49 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                                                                      DisplayAnimatedHistoryMap

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddContent(86, "Afiseaza Animatie Istoric");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(87, "\r\n                    ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(88, "\r\n                ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(89, "\r\n            ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(90, "\r\n        ");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(91, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(92, "\r\n    ");
            __builder.OpenComponent<GoogleMapsComponents.GoogleMap>(93);
            __builder.AddAttribute(94, "Id", "map1");
            __builder.AddAttribute(95, "Options", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<GoogleMapsComponents.Maps.MapOptions>(
#nullable restore
#line 55 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                                                        this.mapOptions

#line default
#line hidden
#nullable disable
            ));
            __builder.AddComponentReferenceCapture(96, (__value) => {
#nullable restore
#line 55 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
                       this.map1 = (GoogleMapsComponents.GoogleMap)__value;

#line default
#line hidden
#nullable disable
            }
            );
            __builder.CloseComponent();
            __builder.AddMarkupContent(97, "\r\n");
#nullable restore
#line 56 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 58 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\History.razor"
       
    LocationDisplay.PageControllers.HistoryController historyController = new PageControllers.HistoryController();
    List<PublicObjects.LocationHistory> locations = new List<PublicObjects.LocationHistory>();
    List<PublicObjects.AnimatedLocationHistory> animatedLocations = new List<PublicObjects.AnimatedLocationHistory>();
    /*
    List<DatabaseConnection.DatabaseObjects.ServerLocation> locations = new List<DatabaseConnection.DatabaseObjects.ServerLocation>();
    GoogleMapsComponents.Maps.Marker marker;
    Polyline polyline;
    */

    public static List<Polyline> polylines = new List<Polyline>();


    private GoogleMap map1 = new GoogleMap();
    private MapOptions mapOptions = new MapOptions() {
        Zoom = 15,
        Center = new LatLngLiteral()
        {
            Lat = 44.18989707,
            Lng = 28.62403504
        },
        MapTypeId = MapTypeId.Roadmap
    };

    private void ValidateFilter()
    {
        historyController.CheckDate();
        DisplayHistoryMap();
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => DisplayHistoryMap());
    }

    private void RetrieveSettings()
    {
        List<DatabaseConnection.DatabaseObjects.ServerLocation> locationList = new List<DatabaseConnection.DatabaseObjects.ServerLocation>();
        DatabaseConnection.DatabaseLink.LocationFunctions.RetrieveSpecificLocationHistory(locationList, historyController);
        locations = PublicObjects.LocationHistory.GetLocationsFromServer(locationList);
        map1.InteropObject.PanTo(PublicObjects.LocationHistory.GetCenterOfLocations(locationList));

    }
    private async void DisplayHistoryPolylines()
    {
        foreach (var polilyne in polylines) await polilyne.SetMap(null);
        Int32 displayColor = 0;
        foreach(PublicObjects.LocationHistory location in locations)
        {
            polylines.Add(await Polyline.CreateAsync(map1.JsRuntime,
                new PolylineOptions()
                {
                    StrokeColor = Miscellaneous.ColorSettings.getColorOfPolygonColor(displayColor).Name,
                    StrokeOpacity = 1,
                    StrokeWeight = 1,
                    Map = map1.InteropObject,
                    Path = location.getPolylineList()
                }
                )) ;
            displayColor++;
        }
    }
    private async void DisplayHistoryMap()
    {
        await Task.Run(() => RetrieveSettings()).ContinueWith(t=>DisplayHistoryPolylines());
    }
    private async void DisplayAnimatedHistoryMap()
    {
        await Task.Run(() => RetrieveSettings()).ContinueWith(t=>InitializePolylines());
    }

    private async void InitializePolylines()
    {
        foreach (var polilyne in polylines) await polilyne.SetMap(null);
        if(animatedLocations != null)
        {
            foreach (var element in animatedLocations) await Task.Run(() =>
            {
                if(element.polyline != null) element.polyline.SetMap(null);
                if(element.marker != null) element.marker.SetMap(null);
            });
        }
        Int32 displayColor = 0;
        foreach(var location in locations)
        {
            animatedLocations.Add(new PublicObjects.AnimatedLocationHistory()
            {
                AgentCode = location.AgentCode,
                PolylineColor = Miscellaneous.ColorSettings.getColorOfPolygonColor(displayColor).Name,
                completePath = location.getPolylineList()

            });
            displayColor++;
        }
        foreach(var element in animatedLocations)
        {
            await Task.Run(()=>AnimatePolyline(element));
        }
    }

    private async void AnimatePolyline(PublicObjects.AnimatedLocationHistory animatedLocationHistory)
    {
        foreach(var element in animatedLocationHistory.completePath)
        {
            if (animatedLocationHistory.polyline == null)
            {
                animatedLocationHistory.polyline = await Polyline.CreateAsync(map1.JsRuntime, new PolylineOptions()
                {
                    StrokeColor = animatedLocationHistory.PolylineColor,
                    StrokeWeight = 2,
                    Map = map1.InteropObject
                });
            }
            else
            {
#pragma warning disable CS4014
                animatedLocationHistory.polyline.SetPath(animatedLocationHistory.currentPath);
#pragma warning restore
            }
            if(animatedLocationHistory.marker == null)
            {
                animatedLocationHistory.marker = await Marker.CreateAsync(map1.JsRuntime, new MarkerOptions()
                {
                    Position = new LatLngLiteral()
                    {
                        Lat = element.Lat,
                        Lng = element.Lng
                    },
                    Map = map1.InteropObject
                });
            }
            else
            {
#pragma warning disable CS4014
                animatedLocationHistory.marker.SetPosition(new LatLngLiteral()
                {
                    Lat = element.Lat,
                    Lng = element.Lng
                });
#pragma warning restore
            }
            animatedLocationHistory.currentPath.Add(new LatLngLiteral()
            {
                Lat = element.Lat,
                Lng = element.Lng
            });
            System.Threading.Thread.Sleep(100);
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
