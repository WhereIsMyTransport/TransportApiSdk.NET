using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Stop
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public Agency Agency { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public GeoJsonPoint Geometry { get; set; }
        public List<TransportMode> Modes { get; set; }
        public Stop ParentStop { get; set; }
    }
}