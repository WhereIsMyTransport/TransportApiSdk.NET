using System;
using System.Collections.Generic;
using System.Text;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Shape
    {
        public string StopId { get; set; }
        public GeoJsonLineString Geometry { get; set; }
        public Distance CumulativeDistance { get; set; }
    }
}
