using System;
using System.Collections.Generic;
using System.Text;

namespace TransportApi.Sdk.Models.ResultModels
{
    public class ScheduleInstance
    {
        public string Name { get; set; }
        public List<Interval> Intervals { get; set; }
    }
}
