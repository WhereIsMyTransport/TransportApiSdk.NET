namespace TransportApi.Shared.Models.Enums
{
    public enum EventType
    {
        Undefined,
        /// <summary>
        /// Departure specifies that departing timetables are returned.
        /// </summary>
        Departure,
        /// <summary>
        /// Arrival specifies that arriving timetables are returned.
        /// </summary>
        Arrival
    }
}
