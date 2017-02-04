using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.InputModels
{
    internal class JourneyOnlyInputModel
    {
        public IEnumerable<string> Agencies { get; set; }
        public IEnumerable<TransportMode> Modes { get; set; }
    }
}
