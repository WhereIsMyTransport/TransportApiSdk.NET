using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class GeoJsonPoint
    {
        public GeoJsonType Type { get; set; }

        /// <summary>
        /// Note: GeoJSON represents geographic coordinates with longitude first and then latitude, [longitude, latitude]. i.e. [x, y] in the Cartesian coordinate system.
        /// </summary>
        public List<string> Coordinates { get; set; }
    }
}
