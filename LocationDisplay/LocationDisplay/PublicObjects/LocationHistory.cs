using GoogleMapsComponents.Maps;
using LocationDisplay.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.PublicObjects
{
    /// <summary>
    /// the main location settings for displaying on map
    /// </summary>
    public class LocationHistory
    {
        public String AgentCode { get; set; } = String.Empty;
        public List<LocationSettings> locationSettings = new List<LocationSettings>();

        public static LatLngLiteral GetCenterOfLocations(List<DatabaseObjects.ServerLocation> locations)
        {
            return new LatLngLiteral
            {
                Lat = locations.Sum(x => x.Latitude) / locations.Count(),
                Lng = locations.Sum(x=>x.Longitude) / locations.Count()
            };
        }
        public List<LatLngLiteral> getPolylineList()
        {
            List<LatLngLiteral> pointList = new List<LatLngLiteral>();
            foreach(LocationSettings locationSettings in locationSettings)
            {
                pointList.Add(new LatLngLiteral() 
                {
                    Lat = locationSettings.Latitude,
                    Lng = locationSettings.Longitude
                });
            }
            return pointList;
        }
        public static List<LocationHistory> GetLocationsFromServer(List<DatabaseObjects.ServerLocation> locations)
        {
            List<LocationHistory> agentsHistory = new List<LocationHistory>();
            foreach(var agentCode in locations.Select(x => x.AgentCode).Distinct())
            {
                agentsHistory.Add(new LocationHistory
                {
                    AgentCode = agentCode,
                    locationSettings = locations.Where(x => x.AgentCode == agentCode)
                                            .Select(x => new LocationSettings
                                            {
                                                Accuaracy = x.Accuaracy,
                                                Altitude = x.Altitude,
                                                Latitude = x.Latitude,
                                                Longitude = x.Longitude,
                                                Bearing = x.Bearing,
                                                LastUpdate = x.LastUpdate,
                                                Speed = x.Speed
                                            }).ToList()
                });
            }
            return agentsHistory;
        }
    }
    /// <summary>
    /// the location settings for any given specific point
    /// </summary>
    public class LocationSettings
    {
        public Double Latitude { get; set; } = new Double();
        public Double Longitude { get; set; } = new Double();
        public Double Altitude { get; set; } = new Double();
        public Double Accuaracy { get; set; } = new Double();
        public Double Bearing { get; set; } = new Double();
        public Double Speed { get; set; } = new Double();
        public DateTime LastUpdate { get; set; } = new DateTime();
    }
}
