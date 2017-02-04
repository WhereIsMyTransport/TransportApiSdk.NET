namespace TransportApi.Sdk.Models.ResultModels
{
    public class Location
    {
        public string Address { get; set; }
        public GeoJsonPoint Geometry { get; set; }
    }
}