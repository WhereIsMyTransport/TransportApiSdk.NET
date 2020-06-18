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
    public class JourneyTests
    {
        TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = ClientCredentials.ClientId,
            ClientSecret = ClientCredentials.ClientSecret
        });

        private static CancellationToken defaultCancellationToken = CancellationToken.None;
        private static IEnumerable<string> defaultOnlyAgencies = null;
        private static IEnumerable<string> defaultOmitAgencies = null;
        private static IEnumerable<string> defaultFareProducts = null;
        private static DateTime? defaultAt = null;
        private static IEnumerable<TransportMode> defaultOnlyModes = new List<TransportMode>() { TransportMode.Bus };
        private static IEnumerable<TransportMode> defaultOmitModes = null;
        private static DateTime? defaultTime = DateTime.UtcNow; //.AddHours(16);
        private static TimeType defaultTimeType = TimeType.DepartAfter;
        private static string defaultProfile = "FewestTransfers";
        /*private static double defaultStartLatitude = -25.747562;
        private static double defaultStartLongitude = 28.236323;
        private static double defaultEndLatitude = -26.195135;
        private static double defaultEndLongitude = 28.036299;*/

        /*private static double defaultStartLatitude = -25.788202;
        private static double defaultStartLongitude = 28.275035;
        private static double defaultEndLatitude = -26.147182;
        private static double defaultEndLongitude = 28.04509;*/

        private static double defaultStartLatitude = -33.9243062;
        private static double defaultStartLongitude = 18.4279828;
        private static double defaultEndLatitude = -33.9256371;
        private static double defaultEndLongitude = 18.4351148;

        private static string exlude = "geometry";

        [TestMethod]
        public async Task PostJourneyAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude);

            Assert.IsTrue(results.IsSuccess);
        }

        [TestMethod]
        public async Task PostJourneyItineraryAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);

            if (results.IsSuccess && results.Data != null)
            {
                Assert.IsNotNull(results.Data.Itineraries);
                Assert.IsTrue(results.Data.Itineraries.Any());

                if (results.Data.Itineraries != null && results.Data.Itineraries.Any())
                {
                    var itinerary = await defaultClient.GetJourneyItineraryAsync(defaultCancellationToken, results.Data.Id, results.Data.Itineraries.First().Id);

                    Assert.IsTrue(itinerary.IsSuccess);
                    Assert.IsNotNull(itinerary.Data);
                }
            }
        }

        [TestMethod]
        public async Task GetJourneyAsync_ValidInputs_IsSuccess()
        {
            var journey = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, defaultTime, defaultFareProducts, defaultOnlyAgencies, defaultOmitAgencies, defaultOnlyModes, defaultOmitModes, defaultTimeType, defaultProfile, exclude: exlude);

            var results = await defaultClient.GetJourneyAsync(defaultCancellationToken, journey.Data.Id);

            Assert.IsTrue(results.IsSuccess);
            Assert.IsNotNull(results.Data);
        }

        [TestMethod]
        public async Task PostJourneyAsync_ExcludeAgency_IsSuccess()
        {
            IEnumerable<string> excludeGautrain = new List<string>()
            {
                "edObkk6o-0WN3tNZBLqKPg"
            };

            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, defaultTime, defaultFareProducts, defaultOnlyAgencies, excludeGautrain, defaultOnlyModes, defaultOmitModes, defaultTimeType, defaultProfile);

            Assert.IsTrue(results.IsSuccess);

            var hasGautrain = results.Data.Itineraries.SelectMany(x => x.Legs.Where(y => y.Line != null).Select(y => y.Line)).Any(x => x.Agency.Id == excludeGautrain.First());

            Assert.IsFalse(hasGautrain);
        }

        [TestMethod]
        public async Task PostJourneyAsync_LimitAgency_IsSuccess()
        {
            IEnumerable<string> limitGautrain = new List<string>()
            {
                "edObkk6o-0WN3tNZBLqKPg"
            };

            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, defaultTime, defaultFareProducts, limitGautrain, defaultOmitAgencies, defaultOnlyModes, defaultOmitModes, defaultTimeType, defaultProfile);

            Assert.IsTrue(results.IsSuccess);

            var otherAgencies = results.Data.Itineraries.SelectMany(x => x.Legs.Where(y => y.Line != null).Select(y => y.Line)).Any(x => x.Agency.Id != limitGautrain.First());

            Assert.IsFalse(otherAgencies);
        }

        [TestMethod]
        public async Task PostJourneyAsync_ExcludeMode_IsSuccess()
        {
            IEnumerable<TransportMode> noRailMode = new List<TransportMode>()
            {
                TransportMode.Rail
            };

            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, defaultTime, defaultFareProducts, defaultOnlyAgencies, defaultOmitAgencies, defaultOnlyModes, noRailMode, defaultTimeType, defaultProfile);

            Assert.IsTrue(results.IsSuccess);

            var hasRail = results.Data.Itineraries.SelectMany(x => x.Legs.Where(y => y.Line != null).Select(y => y.Line)).Any(x => x.Mode == TransportMode.Rail);

            Assert.IsFalse(hasRail);
        }

        [TestMethod]
        public async Task PostJourneyAsync_NonDefaultProduct_IsSuccess()
        {
            IEnumerable<string> limitGautrain = new List<string>()
            {
                "edObkk6o-0WN3tNZBLqKPg"
            };

            var fareProducts = await defaultClient.GetFareProductsAsync(defaultCancellationToken, limitGautrain, defaultOmitAgencies);

            Assert.IsTrue(fareProducts.IsSuccess);
            Assert.IsNotNull(fareProducts.Data);

            if (fareProducts.IsSuccess && fareProducts.Data != null)
            {
                var nonDefaultFareProduct = new List<string>()
                {
                    fareProducts.Data.First(x => x.IsDefault == false).Id
                };

                var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, defaultTime, nonDefaultFareProduct, defaultOnlyAgencies, defaultOmitAgencies, defaultOnlyModes, defaultOmitModes, defaultTimeType, defaultProfile);

                Assert.IsTrue(results.IsSuccess);
            }
        }
    }
}
