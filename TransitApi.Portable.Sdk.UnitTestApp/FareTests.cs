using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk;
using System.Linq;

namespace TransportApi.Portable.Sdk.UnitTestApp
{
    [TestClass]
    public class FareTests
    {
        TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = ClientCredentials.ClientId,
            ClientSecret = ClientCredentials.ClientSecret
        });

        private static string defaultGautrainAgencyId = "edObkk6o-0WN3tNZBLqKPg";
        private static CancellationToken defaultCancellationToken = CancellationToken.None;
        private static IEnumerable<string> defaultLimitAgencies = null;
        private static IEnumerable<string> defaultExcludeAgencies = null;
        private static DateTime? defaultAt = null;

        [TestMethod]
        public async Task GetFareProductsAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetFareProductsAsync(defaultCancellationToken, defaultLimitAgencies, defaultExcludeAgencies);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }

        [TestMethod]
        public async Task GetFareTablesAsync_ValidInputs_IsSuccess()
        {
            var limitAgencyToGautrain = new List<string>()
            {
                defaultGautrainAgencyId
            };

            var gautrainFareProducts = await defaultClient.GetFareProductsAsync(defaultCancellationToken, limitAgencyToGautrain, defaultExcludeAgencies);

            var defaultProduct = gautrainFareProducts.Data.Single(x => x.IsDefault);

            var results = await defaultClient.GetFareTablesAsync(defaultCancellationToken, defaultProduct.Id);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }

        [TestMethod]
        public async Task GetFareProductAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.GetFareProductAsync(defaultCancellationToken, defaultGautrainAgencyId);

            Assert.IsTrue(results.IsSuccess);
        }
    }
}
