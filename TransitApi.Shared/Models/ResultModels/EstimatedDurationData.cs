using System;
using System.Collections.Generic;
using System.Text;

namespace TransportApi.Sdk.Models.ResultModels
{
    /// <summary>
    /// Experimental Feature Model
    /// </summary>
    public class EstimatedDurationData
    {
        public double Headway { get; set; }

        public double HeadwayUpperBoundTotal { get; set; }

        public double TransitDuration { get; set; }

        public double TransitUpperBoundExtra { get; set; }
    }
}