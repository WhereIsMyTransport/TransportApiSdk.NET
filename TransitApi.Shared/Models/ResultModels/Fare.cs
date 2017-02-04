using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Fare
    {
        public string Description { get; set; }
        public FareProduct FareProduct { get; set; }
        public Cost Cost { get; set; }
        public List<string> Messages { get; set; }
    }
}