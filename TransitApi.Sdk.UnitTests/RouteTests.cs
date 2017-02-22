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
            var allLines = await defaultClient.GetLinesAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesStops);

            Assert.IsTrue(allLines.IsSuccess);
            Assert.IsNotNull(allLines.Data);

            if (allLines.IsSuccess && allLines.Data != null)
            {
                var results = await defaultClient.GetRoutesByLineAsync(defaultCancellationToken, allLines.Data.First().Id);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }

        }
    }
}
