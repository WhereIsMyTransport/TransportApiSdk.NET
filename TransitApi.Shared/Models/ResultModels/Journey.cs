using System;
using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Journey
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public GeoJsonLineString Geometry { get; set; }
        public DateTime EarliestDepartureTime { get; set; }
        public DateTime LatestArrivalTime { get; set; }
        public int MaxItineraries { get; set; }
        public List<TransportMode> Modes { get; set; }
        public List<string> Agencies { get; set; }
        public List<Itinerary> Itineraries { get; set; }
    }
}
