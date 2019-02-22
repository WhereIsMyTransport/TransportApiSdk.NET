using System;
using System.Collections.Generic;
using System.Text;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class Frequency
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int HeadWayInSeconds { get; set; }
        public bool IsFixedTimes { get; set; }
    }
}