namespace TransportApi.Sdk.Models.Enums
{
    public enum Behaviour
    {
        Undefined,
        /// <summary>
        /// A Static leg is one where the given times are from a frequency-based or static timetable, where the intention is that the vehicle stops at the time given in the waypoint. Whether the vehicle adheres to this time in reality can still be dependent on delays or other external factors.
        /// </summary>
        Static,
        /// <summary>
        /// An Estimated leg is one where the given times are an estimate based on the probability of a vehicle arriving at or before that time to a reasonable confidence level. Estimated legs result from travel on a route without exact times nor formal timetables.
        /// </summary>
        Estimated
    }
}
