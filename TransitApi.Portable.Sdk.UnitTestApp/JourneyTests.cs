using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk;
using TransportApi.Sdk.Models.Enums;
using System.Linq;

namespace TransportApi.Portable.Sdk.UnitTestApp
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
        private static IEnumerable<TransportMode> defaultOnlyModes = null;
        private static IEnumerable<TransportMode> defaultOmitModes = null;
        private static DateTime? time = DateTime.Now;
        private static TimeType timeType = TimeType.DepartAfter;
        private static Profile profile = Profile.ClosestToTime;
        private static double defaultStartLatitude = -25.747562;
        private static double defaultStartLongitude = 28.236323;
        private static double defaultEndLatitude = -26.195135;
        private static double defaultEndLongitude = 28.036299;
        private static DateTime? defaultAt = null;

        [TestMethod]
        public async Task PostJourneyAsync_ValidInputs_IsSuccess()
        {
            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, time, defaultFareProducts, defaultOnlyAgencies, defaultOmitAgencies, defaultOnlyModes, defaultOmitModes, timeType, profile);

            Assert.IsTrue(results.IsSuccess);
        }

        [TestMethod]
        public async Task PostJourneyAsync_ExcludeAgency_IsSuccess()
        {
            IEnumerable<string> excludeGautrain = new List<string>()
            {
                "edObkk6o-0WN3tNZBLqKPg"
            };

            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, time, defaultFareProducts, defaultOnlyAgencies, excludeGautrain, defaultOnlyModes, defaultOmitModes, timeType, profile);

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

            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, time, defaultFareProducts, limitGautrain, defaultOmitAgencies, defaultOnlyModes, defaultOmitModes, timeType, profile);

            Assert.IsTrue(results.IsSuccess);

            var otherAgencies = results.Data.Itineraries.SelectMany(x => x.Legs.Where(y => y.Line != null).Select(y => y.Line)).Any(x => x.Agency.Id != limitGautrain.First());

            Assert.IsFalse(otherAgencies);
        }

        [TestMethod]
        public async Task PostJourneyAsync_ExcludeMode_IsSuccess()
        {
            IEnumerable<TransportMode> noRailMode = new List<TransportMode>()
            {
                TransportMode.Air,
                TransportMode.Bus,
                TransportMode.Coach,
                TransportMode.Ferry,
                TransportMode.Funicular,
                TransportMode.Gondola,
                TransportMode.GroundCableCar,
                TransportMode.LightRail,
                TransportMode.Subway
            };

            var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, time, defaultFareProducts, defaultOnlyAgencies, defaultOmitAgencies, noRailMode, defaultOmitModes, timeType, profile);

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

                var results = await defaultClient.PostJourneyAsync(defaultCancellationToken, defaultStartLatitude, defaultStartLongitude, defaultEndLatitude, defaultEndLongitude, time, nonDefaultFareProduct, defaultOnlyAgencies, defaultOmitAgencies, defaultOnlyModes, defaultOmitModes, timeType, profile);

                Assert.IsTrue(results.IsSuccess);
            }
        }
    }
}
