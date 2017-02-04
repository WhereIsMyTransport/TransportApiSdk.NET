using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk.Models.Enums;

namespace TransportApi.Sdk.UnitTests
{
    [TestClass]
    public class RouteTests
    {
        TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = ClientCredentials.ClientId,
            ClientSecret = ClientCredentials.ClientSecret
        });

        private static CancellationToken defaultCancellationToken = CancellationToken.None;
        private static IEnumerable<string> defaultLimitAgencies = null;
        private static IEnumerable<string> defaultExcludeAgencies = null;
        private static IEnumerable<TransportMode> defaultModes = null;
        private static IEnumerable<string> defaultServesStops = null;
        private static DateTime? defaultAt = null;

        [TestMethod]
        public async Task GetRoutesByLineAsync_ValidInputs_IsSuccess()
        {
            var allLines = await defaultClient.GetLinesAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesStops, defaultAt);

            Assert.IsTrue(allLines.IsSuccess);
            Assert.IsNotNull(allLines.Data);

            if (allLines.IsSuccess && allLines.Data != null)
            {
                var results = await defaultClient.GetRoutesByLineAsync(defaultCancellationToken, "0503e0b5-49bd-443c-83b5-0556d8187e51", allLines.Data.First().Id, defaultAt);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }

        }
    }
}
