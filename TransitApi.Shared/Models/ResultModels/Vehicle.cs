using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Vehicle
    {
        public string Designation { get; set; }
        public string Direction { get; set; }
        public string Headsign { get; set; }
        public string TripId { get; set; }
        public int TripNumber { get; set; }
        public List<AlternativeVehicle> AlternativeVehicles { get; set; }
    }
}