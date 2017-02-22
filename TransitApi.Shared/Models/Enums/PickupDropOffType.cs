namespace TransportApi.Sdk.Models.Enums
{
    public enum PickupDropOffType
    {
        /// <summary>
        /// Indicates that the vehicle may pick up (or drop off) passengers at this waypoint.
        /// </summary>
        Always,
        /// <summary>
        /// A value of OnRequest indicates that the passenger must specially arrange to be picked up (or dropped off) at this waypoint, such as by phone or by coordinating with the driver.
        /// </summary>
        OnRequest,
        /// <summary>
        /// A value of Never indicates that the vehicle does not pick up (or drop off) passengers at this waypoint.
        /// </summary>
        Never
    }
}
