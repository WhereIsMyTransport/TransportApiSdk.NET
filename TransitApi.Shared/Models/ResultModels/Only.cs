using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Only
    {
        public List<TransportMode> Modes { get; set; }
        public List<string> Agencies { get; set; }
    }
}
