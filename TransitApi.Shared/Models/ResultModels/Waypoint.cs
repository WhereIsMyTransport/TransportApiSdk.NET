using System;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Waypoint
    {
        public Location Location { get; set; }
        public Stop Stop { get; set; }
        public Hail Hail { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public PickupDropOffType pickupType { get; set; }
        public PickupDropOffType dropOffType { get; set; }
    }
}