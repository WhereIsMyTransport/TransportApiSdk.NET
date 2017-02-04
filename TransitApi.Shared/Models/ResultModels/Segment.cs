using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Segment
    {
        public Stop DepartureStop { get; set; }
        public Stop ArrivalStop { get; set; }
        public GeoJsonLineString Geometry { get; set; }
        public List<Shape> Shape { get; set; }
        public Distance Distance { get; set; }
    }
}
