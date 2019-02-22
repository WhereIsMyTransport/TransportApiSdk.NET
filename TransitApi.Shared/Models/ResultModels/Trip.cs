using System;
using System.Collections.Generic;
using System.Text;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Trip
    {
        public string TripId { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public Line Line { get; private set; }
        public List<Waypoint> Waypoints { get; private set; }
        public List<Frequency> Frequencies { get; private set; }
        public List<GeoJsonLineString> GeometrySections { get; private set; }
        public string TimeZone { get; private set; }

        public static Trip Hydrate(
            string key,
            Vehicle vehicle,
            Line line,
            List<Waypoint> stopWaypoints,
            List<Frequency> frequencies,
            List<GeoJsonLineString> geometrySections,
            string timeZone)
        {
            return new Trip
            {
                TripId = key,
                Vehicle = vehicle,
                Line = line,
                Frequencies = frequencies,
                GeometrySections = geometrySections,
                TimeZone = timeZone,
                Waypoints = stopWaypoints
            };
        }
    }
}