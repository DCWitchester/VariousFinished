#pragma checksum "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\Location.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d25925c75fd11b7b5aa1d73b3fce40ab72d358f1"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
#line 3 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\Location.razor"
using LocationDisplay.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\Location.razor"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\Location.razor"
using GoogleMapsComponents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\Location.razor"
using GoogleMapsComponents.Maps;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/location")]
    public partial class Location : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 41 "D:\locationDisplay\LocationDisplay\LocationDisplay\Pages\Location.razor"
       
    private GoogleMap map1 = new GoogleMap();

    private MapOptions mapOptions = new MapOptions()
    {
        Zoom = 15,
        Center = new LatLngLiteral()
        {
            Lat = 44.18989707,
            Lng = 28.62403504
        },
        MapTypeId = MapTypeId.Roadmap
    };

    private LocationDisplay.PageControllers.LocationController locationController = new PageControllers.LocationController();
    private List<PublicObjects.CurrentLocation> currentLocations = new List<PublicObjects.CurrentLocation>();

    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => InitializeCurrentLocations());
#pragma warning disable CS4014
        Task.Run(() => DisplayStart());
#pragma warning restore
    }

    private async void InitializeCurrentLocations()
    {
        List<DatabaseConnection.DatabaseObjects.CurrentAgentLocation> currentAgentLocations
                        = new List<DatabaseConnection.DatabaseObjects.CurrentAgentLocation>();
        await Task.Run(()=>ResetMapElements());
        DatabaseConnection.DatabaseLink.LocationFunctions.RetrieveCurrentLocation(locationController, currentAgentLocations);
        currentLocations = PublicObjects.CurrentLocation.GetLocationsFromServer(currentAgentLocations);
        Int32 displayColor = 0;
        foreach (var element in currentLocations)
        {
            element.PathColor = Miscellaneous.ColorSettings.getColorOfPolygonColor(displayColor).Name;
            displayColor++;
        }
    }

    private async void ResetMapElements()
    {
        if(currentLocations!= null)
        {
            foreach (var element in currentLocations) await Task.Run(() =>
            {
                if (element.CurrentPath != null) element.CurrentPath.SetMap(null);
                if (element.CurrentLocationMarker != null) element.CurrentLocationMarker.SetMap(null);
            });
            currentLocations = new List<PublicObjects.CurrentLocation>();
        }
    }

    private async void DisplayStart()
    {
        do
        {
            foreach (var element in currentLocations)
            {
                await Task.Run(() => InitializeAgentLocation(element)).ContinueWith(t => UpdateAgentLocation(element));
            }
            System.Threading.Thread.Sleep(LocationDisplay.PublicObjects.Settings.AnimationSpeed);
        }while (true);
    }


    private async void InitializeAgentLocation(PublicObjects.CurrentLocation agentLocation)
    {
        if(agentLocation.CurrentLocationMarker == null)
            agentLocation.CurrentLocationMarker = await Marker.CreateAsync(map1.JsRuntime, new MarkerOptions()
            {
                Position = agentLocation.CurrentLocationPoint,
                Map = map1.InteropObject,
                Title = agentLocation.AgentCode
            });
        if(agentLocation.CurrentPath == null) agentLocation.CurrentPath = await Polyline.CreateAsync(map1.JsRuntime, new PolylineOptions()
        {
            StrokeColor = agentLocation.PathColor,
            StrokeWeight = 2,
            Map = map1.InteropObject
        });
    }

#pragma warning disable CS1998
    private async void UpdateAgentLocation(PublicObjects.CurrentLocation agentLocation)
    {
        LatLngLiteral newPoint = DatabaseConnection.DatabaseLink.LocationFunctions.RetrieveCurrentLocation(agentLocation.AgentCode);
        if (newPoint == null || newPoint == agentLocation.CurrentLocationPoint) return;
        else
        {
            agentLocation.PathPoints.Add(agentLocation.CurrentLocationPoint);
            agentLocation.CurrentLocationPoint = newPoint;
#pragma warning disable CS4014
            agentLocation.CurrentLocationMarker.SetPosition(agentLocation.CurrentLocationPoint);
            agentLocation.CurrentPath.SetPath(agentLocation.PathPoints);
#pragma warning restore
        }
    }
#pragma warning restore

    private async void FilterAgentLocation()
    {
        await Task.Run(() => InitializeCurrentLocations());
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591