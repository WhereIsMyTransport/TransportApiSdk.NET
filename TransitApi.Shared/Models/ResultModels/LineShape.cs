using System;
using System.Collections.Generic;
using System.Text;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class LineShape
    {
        public Stop DepartureStop { get; set; }
        public Stop ArrivalStop { get; set; }
        public GeoJsonLineString Geometry { get; set; }
        public Distance Distance { get; set; }
    }
}
