using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.DatabaseConnection
{
    public class DatabaseObjects
    {
        public class ServerLocation
        {
            public Int32 LocationId { get; set; } = new Int32();
            public String AgentCode { get; set; } = String.Empty;
            public Double Latitude { get; set; } = new Double();
            public Double Longitude { get; set; } = new Double();
            public Double Altitude { get; set; } = new Double();
            public Double Accuaracy { get; set; } = new Double();
            public Double Bearing { get; set; } = new Double();
            public Double Speed { get; set; } = new Double();
            public DateTime LastUpdate { get; set; } = new DateTime();
        }

        public class LocationComparer : IEqualityComparer<ServerLocation>
        {
            public bool Equals(ServerLocation x, ServerLocation y)
            {
                return x.Latitude == y.Latitude && 
                    x.Longitude == y.Longitude;
            }

            public int GetHashCode(ServerLocation location)
            {
                return location.LocationId.GetHashCode() ^
                    location.AgentCode.GetHashCode()^
                    location.Latitude.GetHashCode() ^
                    location.Longitude.GetHashCode() ^
                    location.Altitude.GetHashCode()^
                    location.Accuaracy.GetHashCode()^
                    location.Bearing.GetHashCode()^
                    location.Speed.GetHashCode()^
                    location.LastUpdate.GetHashCode();
            }
        }

        public class CurrentAgentLocation
        {
            public String AgentCode { get; set; } = String.Empty;
            public Double Latitude { get; set; } = new Double();
            public Double Longitude { get; set; } = new Double();
        }
    }
}
