using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class LineTimetable
    {
        public Vehicle Vehicle { get; set; }
        public List<Waypoint> Waypoints { get; set; }
    }
}
