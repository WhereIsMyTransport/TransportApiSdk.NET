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
    public class StopTests
    {
        TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = ClientCredentials.ClientId,
            ClientSecret = ClientCredentials.ClientSecret
        });

        private static string defaultBoundingBox = "-33.944,18.36,-33.895,18.43";
        private static string defaultGautrainAgencyId = "edObkk6o-0WN3tNZBLqKPg";
        private static CancellationToken defaultCancellationToken = CancellationToken.None;
        private static IEnumerable<string> defaultLimitAgencies = null;
        private static IEnumerable<string> defaultExcludeAgencies = null;
        private static IEnumerable<TransportMode> defaultModes = null;
        private static IEnumerable<string> defaultServesLines = null;
        private static DateTime? defaultAt = null;
        private static double defaultLatitude = -33.9787343;
        private static double defaultLongitude = -33.9787343;

        [TestMethod]
        public async Task GetStopsAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetStopsAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesLines, defaultAt);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }

        [TestMethod]
        public async Task GetStopsNearbyAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetStopsNearbyAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesLines, defaultAt, defaultLatitude, defaultLongitude);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }

        [TestMethod]
        public async Task GetStopsByBoundingBoxAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetStopsByBoundingBoxAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesLines, defaultAt, defaultBoundingBox);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }
        
        [TestMethod]
        public async Task GetStopAsync_ValidInputs_IsSuccess()
        {
            var allStops = await defaultClient.GetStopsAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesLines, defaultAt);

            Assert.IsTrue(allStops.IsSuccess);
            Assert.IsNotNull(allStops.Data);

            if (allStops.IsSuccess && allStops.Data != null)
            {
                var results = await defaultClient.GetStopAsync(defaultCancellationToken, allStops.Data.First().Id, defaultAt);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }
        }

        [TestMethod]
        public async Task GetStopChildrenAsync_ValidInputs_IsSuccess()
        {
            var allStops = await defaultClient.GetStopsAsync(
                defaultCancellationToken,
                new List<string>() { defaultGautrainAgencyId },
                defaultExcludeAgencies,
                new List<TransportMode>() { TransportMode.Rail },
                defaultServesLines,
                defaultAt);

            Assert.IsTrue(allStops.IsSuccess);
            Assert.IsNotNull(allStops.Data);

            if (allStops.IsSuccess && allStops.Data != null)
            {
                var results = await defaultClient.GetChildStopsAsync(defaultCancellationToken, allStops.Data.First(x => x.Name == "Midrand").Id, defaultAt);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }
        }

        [TestMethod]
        public async Task GetStopsWithChildrenAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetStopsAsync(defaultCancellationToken, new List<string>() { defaultGautrainAgencyId }, defaultExcludeAgencies, new List<TransportMode>() { TransportMode.Rail }, defaultServesLines, defaultAt, showChildren: true);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }
    }
}
