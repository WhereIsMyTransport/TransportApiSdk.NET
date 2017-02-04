using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class FareProduct
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
    }
}