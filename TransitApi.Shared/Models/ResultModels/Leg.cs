using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Leg
    {
        public LegType Type { get; set; }
        public Behaviour Behaviour { get; set; }
        public Distance Distance { get; set; }
        public int Duration { get; set; }
        public Line Line { get; set; }
        public List<Waypoint> Waypoints { get; set; }
        public List<Direction> Directions { get; set; }
        public GeoJsonLineString Geometry { get; set; }
        public Vehicle Vehicle { get; set; }
        public Fare Fare { get; set; }
        public EstimatedDurationData EstimatedDurationData { get; set; }
    }
}