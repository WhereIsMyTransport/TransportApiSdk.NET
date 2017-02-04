namespace TransportApi.Sdk.Models.ResultModels
{
    public class FareTableEntry
    {
        public Zone DepartureZone { get; set; }
        public Zone ArrivalZone { get; set; }
        public Cost Cost { get; set; }
    }
}
