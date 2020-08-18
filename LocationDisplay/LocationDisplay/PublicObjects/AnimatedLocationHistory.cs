using GoogleMapsComponents.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.PublicObjects
{
    public class AnimatedLocationHistory
    {
        public String AgentCode { get; set; } = String.Empty;
        public String PolylineColor { get; set; } = String.Empty;
        public Polyline polyline { get; set; }
        public Marker marker { get; set; }
        public List<LatLngLiteral> completePath { get; set; } = new List<LatLngLiteral>();
        public List<LatLngLiteral> currentPath { get; set; } = new List<LatLngLiteral>();
    }
}
