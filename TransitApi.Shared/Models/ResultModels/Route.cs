using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Route
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string Direction { get; set; }
        public List<Segment> Segments { get; set; }
        public List<TripTemplate> TripTemplates { get; set; }
    }
}
