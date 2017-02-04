using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Line
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public Agency Agency { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public TransportMode Mode { get; set; }
        public string Colour { get; set; }
        public string TextColour { get; set; }
    }
}