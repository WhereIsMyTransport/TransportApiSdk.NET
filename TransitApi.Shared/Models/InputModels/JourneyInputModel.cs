using System.Collections.Generic;
using TransportApi.Sdk.Models.ResultModels;

namespace TransportApi.Sdk.Models.InputModels
{
    internal class JourneyInputModel
    {
        public GeoJsonLineString Geometry { get; set; }
        public string Time { get; set; }
        public string TimeType { get; set; }
        public string Profile { get; set; }
        public JourneyOnlyInputModel Only { get; set; }
        public JourneyOmitInputModel Omit { get; set; }
        public IEnumerable<string> FareProducts { get; set; }
        public int MaxItineraries { get; set; }
    }
}
