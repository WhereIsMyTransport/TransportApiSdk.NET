using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class TripTemplate
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public TransportMode Mode { get; set; }
        public List<StopTime> StopTimes { get; set; }
    }
}
