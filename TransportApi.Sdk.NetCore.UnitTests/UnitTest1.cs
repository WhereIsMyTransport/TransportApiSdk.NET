using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TransportApi.Sdk.NetCore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = ClientCredentials.ClientId,
            ClientSecret = ClientCredentials.ClientSecret
        });

        private static string defaultGautrainAgencyId = "edObkk6o-0WN3tNZBLqKPg";
        private static string defaultBoundingBox = "-33.944,18.36,-33.895,18.43";
        private static CancellationToken defaultCancellationToken = CancellationToken.None;
        private static IEnumerable<string> defaultLimitAgencies = null;
        private static IEnumerable<string> defaultExcludeAgencies = null;
        private static DateTime? defaultAt = null;
        private static double defaultLatitude = -33.9787343;
        private static double defaultLongitude = -33.9787343;

        [TestMethod]
        public async Task GetAgenciesAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetAgenciesAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }
    }
}
