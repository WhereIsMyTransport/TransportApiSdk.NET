using System;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Waypoint
    {
        public Location Location { get; set; }
        public Stop Stop { get; set; }
        public Hail Hail { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}