using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class FareTable
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public List<string> Messages { get; set; }
        public List<FareTableEntry> FareTableEntries { get; set; }
    }
}
