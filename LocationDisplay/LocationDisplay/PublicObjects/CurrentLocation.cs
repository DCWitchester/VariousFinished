using GoogleMapsComponents.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.PublicObjects
{
    public class CurrentLocation
    {
        public String AgentCode { get; set; } = String.Empty;
        public LatLngLiteral CurrentLocationPoint { get; set; } = new LatLngLiteral();
        public List<LatLngLiteral> PathPoints { get; set; } = new List<LatLngLiteral>();
        public String PathColor { get; set; } = String.Empty;
        public Marker CurrentLocationMarker { get; set; }
        public Polyline CurrentPath { get; set; }

        public static List<CurrentLocation> GetLocationsFromServer(List<DatabaseConnection.DatabaseObjects.CurrentAgentLocation> locations)
        {
            List<CurrentLocation> currentLocations = new List<CurrentLocation>();
            foreach(var element in locations)
            {
                currentLocations.Add(
                    new CurrentLocation
                    {
                        AgentCode = element.AgentCode,
                        CurrentLocationPoint = new LatLngLiteral()
                        {
                            Lat = element.Latitude,
                            Lng = element.Longitude
                        }
                    });
            }
            return currentLocations;
        }
    }
}
