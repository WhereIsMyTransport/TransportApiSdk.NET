using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.InputModels
{
    internal class JourneyOmitInputModel
    {
        public IEnumerable<string> Agencies { get; set; }
        public IEnumerable<TransportMode> Modes { get; set; }
    }
}
