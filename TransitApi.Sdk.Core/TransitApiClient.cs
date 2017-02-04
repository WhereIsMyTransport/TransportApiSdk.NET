using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransitApi.Sdk.Components;
using TransitApi.Shared;
using TransitApi.Shared.Models.Enums;
using TransitApi.Shared.Models.ResultModels;
using static TransitApi.Sdk.Components.TransitApiComponent;

namespace TransitApi.Sdk.Core
{
    public class TransitApiClient
    {
        private TokenComponent tokenComponent;
        private TransitApiClientSettings settings;

        public TransitApiClient(TransitApiClientSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "Settings cannot be null");
            }

            this.settings = settings;
            tokenComponent = new TokenComponent(settings.ClientId, settings.ClientSecret);
        }

        /// <summary>
        /// Gets a list of agencies nearby ordered by distance from the point specified.
        /// </summary>
        /// <param name="agencies">Optional list of agencies to filter the results by.</param>
        /// <param name="at">The point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="radiusInMeters">Optional radius to filter results by. Default (-1) is no radius.</param>
        /// <param name="limit">The maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">The offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransitApiResult<IEnumerable<Agency>>> GetAgenciesNearbyAsync(CancellationToken ct, IEnumerable<string> agencies, DateTime? at, double latitude, double longitude, int radiusInMeters = -1, int limit = 100, int offset = 0)
        {
            return await GetAgencies(tokenComponent, settings, ct, agencies, at, latitude, longitude, string.Empty, radiusInMeters, limit, offset);
        }

        /// <summary>
        /// Gets a list of all agencies in the system.
        /// </summary>
        /// <param name="agencies">Optional list of agencies to filter the results by.</param>
        /// <param name="at">The point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">The maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">The offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransitApiResult<IEnumerable<Agency>>> GetAgenciesAsync(CancellationToken ct, IEnumerable<string> agencies, DateTime? at, int limit = 100, int offset = 0)
        {
            return await GetAgencies(tokenComponent, settings, ct, agencies, at, double.NaN, double.NaN, string.Empty, limit: limit, offset: offset);
        }

        /// <summary>
        /// Gets a list of all agencies within a bounding box.
        /// </summary>
        /// <param name="agencies">Optional list of agencies to filter the results by.</param>
        /// <param name="at">The point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="boundingBox">A comma-separated SW (south west) latitude, SW longitude, NE (north east) latitude and NE longitude in that order. Example: -33.944,18.36,-33.895,18.43</param>
        /// <param name="limit">The maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">The offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransitApiResult<IEnumerable<Agency>>> GetAgenciesByBoundingBoxAsync(CancellationToken ct, IEnumerable<string> agencies, DateTime? at, string boundingBox, int limit = 100, int offset = 0)
        {
            return await GetAgencies(tokenComponent, settings, ct, agencies, at, double.NaN, double.NaN, boundingBox, limit: limit, offset: offset);
        }

        /// <summary>
        /// Get a list of itineraties going from A to B.
        /// </summary>
        /// <param name="agencies">Optional list of agencies to filter the results by.</param>
        /// <param name="modes">Optional list of modes to filter the results by.</param>
        /// <param name="earliestDepartureTime">The earliest desired departure date and time for the journey.</param>
        /// <param name="latestArrivalTime">The latest desired arrival date and time for the journey.</param>
        /// <param name="maxItineraries">The maximum number of itineraries to return. Valid values: 1 - 5.</param>
        /// <returns></returns>
        public async Task<TransitApiResult<Journey>> PostJourneyAsync(CancellationToken ct, IEnumerable<string> agencies, IEnumerable<TransitMode> modes, double startLatitude, double startLongitude, double endLatitude, double endLongitude, DateTime? earliestDepartureTime, DateTime? latestArrivalTime, int maxItineraries = 3)
        {
            return await PostJourney(tokenComponent, settings, ct, agencies, modes, startLatitude, startLongitude, endLatitude, endLongitude, earliestDepartureTime, latestArrivalTime, maxItineraries);
        }
    }
}
