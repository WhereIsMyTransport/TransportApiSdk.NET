using System;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class StopTimetable
    {
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public Line Line { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
