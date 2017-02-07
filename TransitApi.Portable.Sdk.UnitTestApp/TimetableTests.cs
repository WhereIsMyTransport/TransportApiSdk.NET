using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Portable.Sdk.UnitTestApp
{
    [TestClass]
    public class TimetableTests
    {
        TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = ClientCredentials.ClientId,
            ClientSecret = ClientCredentials.ClientSecret
        });

        private static CancellationToken defaultCancellationToken = CancellationToken.None;
        private static DateTime? defaultAt = null;
        private static DateTime? defaultEarliestArrivalTime = null;
        private static DateTime? defaultLatestArrivalTime = null;
        private static DateTime? defaultLatestDepartureTime = null;
        private static DateTime? defaultEarliestDepartureTime = null;
        private static string defaultDepartureStopId = null;
        private static string defaultArrivalStopId = null;
        private static IEnumerable<string> defaultLimitAgencies = null;
        private static IEnumerable<string> defaultExcludeAgencies = null;
        private static IEnumerable<TransportMode> defaultModes = null;
        private static IEnumerable<string> defaultServesStops = null;
        private static IEnumerable<string> defaultServesLines = null;

        [TestMethod]
        public async Task GetStopTimetableAsync_ValidInputs_IsSuccess()
        {
            var allStops = await defaultClient.GetStopsAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesLines);

            Assert.IsTrue(allStops.IsSuccess);
            Assert.IsNotNull(allStops.Data);

            if (allStops.IsSuccess && allStops.Data != null)
            {
                var results = await defaultClient.GetStopTimetableAsync(defaultCancellationToken, allStops.Data.First().Id, defaultEarliestArrivalTime, defaultLatestArrivalTime);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }
        }

        [TestMethod]
        public async Task GetLineTimetableAsync_ValidInputs_IsSuccess()
        {
            var allLines = await defaultClient.GetLinesAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesStops);

            Assert.IsTrue(allLines.IsSuccess);
            Assert.IsNotNull(allLines.Data);

            if (allLines.IsSuccess && allLines.Data != null)
            {
                var results = await defaultClient.GetLineTimetableAsync(defaultCancellationToken, allLines.Data.First().Id, defaultDepartureStopId, defaultArrivalStopId, defaultEarliestDepartureTime, defaultLatestDepartureTime);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }
        }
    }
}
