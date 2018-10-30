namespace TransportApi.Sdk.Models.ResultModels
{
    /// <summary>
    /// Experimental Feature Model
    /// </summary>
    public class AlternativeVehicle
    {
        public string TripId { get; set; }
        public int TripNumber { get; set; }
        public int Headway { get; set; }
    }
}