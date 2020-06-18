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
        public DateTime Time { get; set; }
        public TimeType TimeType { get; set; }
        public string Profile { get; set; }
        public int MaxItineraries { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public Only Only { get; set; }
        public Omit Omit { get; set; }
    }
}
