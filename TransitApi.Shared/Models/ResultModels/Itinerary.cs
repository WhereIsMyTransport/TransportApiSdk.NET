using System;
using System.Collections.Generic;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Itinerary
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Duration { get; set; }
        public Distance Distance { get; set; }
        public List<Leg> Legs { get; set; }
    }
}