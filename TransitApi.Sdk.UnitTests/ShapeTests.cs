using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk;
using System.Collections.Generic;
using TransportApi.Sdk.Models.Enums;
using System.Linq;

namespace TransportApi.Sdk.UnitTests
{
    [TestClass]
    public class ShapeTests
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
        public async Task GetLineShapeAsync_ValidInputs_IsSuccess()
        {
            var allLines = await defaultClient.GetLinesAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies, defaultModes, defaultServesStops, defaultAt);

            Assert.IsTrue(allLines.IsSuccess);
            Assert.IsNotNull(allLines.Data);

            if (allLines.IsSuccess && allLines.Data != null)
            {
                var results = await defaultClient.GetLineShapeAsync(defaultCancellationToken, allLines.Data.First().Id, defaultAt);

                Assert.IsTrue(results.IsSuccess);
                Assert.IsNotNull(results.Data);
            }

        }
    }
}
