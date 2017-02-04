using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk.Interfaces;
using TransportApi.Sdk.Models.Enums;
using TransportApi.Sdk.Models.ResultModels;

namespace TransportApi.Sdk
{
    public abstract class AbstractTransportApiClient
    {
        private ITokenComponent tokenComponent;
        private ITransportApiComponent transitApiComponent;
        private TransportApiClientSettings settings;

        internal AbstractTransportApiClient(TransportApiClientSettings settings, ITransportApiComponent transitApiComponent, ITokenComponent tokenComponent)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "Settings cannot be null");
            }

            this.settings = settings;
            this.transitApiComponent = transitApiComponent;
            this.tokenComponent = tokenComponent;
        }

        #region Agencies

        /// <summary>
        /// Gets a list of agencies nearby ordered by distance from the point specified.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="radiusInMeters">Optional radius to filter results by. Default (-1) is no radius.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Agency>>> GetAgenciesNearbyAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, double latitude, double longitude, int radiusInMeters = -1, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetAgencies(tokenComponent, settings, ct, onlyAgencies, omitAgencies, at, latitude, longitude, string.Empty, radiusInMeters, limit, offset);
        }

        
        /// <summary>
        /// Gets a list of all agencies in the system.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Agency>>> GetAgenciesAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetAgencies(tokenComponent, settings, ct, onlyAgencies, omitAgencies, at, double.NaN, double.NaN, string.Empty, limit: limit, offset: offset);
        }

        /// <summary>
        /// Gets a list of all agencies within a bounding box.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="boundingBox">Required comma-separated SW (south west) latitude, SW longitude, NE (north east) latitude and NE longitude in that order. Example: -33.944,18.36,-33.895,18.43</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Agency>>> GetAgenciesByBoundingBoxAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, string boundingBox, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetAgencies(tokenComponent, settings, ct, onlyAgencies, omitAgencies, at, double.NaN, double.NaN, boundingBox, limit: limit, offset: offset);
        }

        /// <summary>
        /// Gets the details of an Agency.
        /// </summary>
        /// <param name="id">Id of the Agency to get.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<Agency>> GetAgencyAsync(CancellationToken ct, string id, DateTime? at)
        {
            return await transitApiComponent.GetAgency(tokenComponent, settings, ct, id, at);
        }

        #endregion

        #region Stops

        /// <summary>
        /// Gets a list of stops nearby ordered by distance from the point specified.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="limitModes">Optional list of modes to filter the results by.</param>
        /// <param name="servesLines">Optional list of lines to filter the results by.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="showChildren">Optionally adds in the children stops for this call.</param>
        /// <param name="radiusInMeters">Optional radius to filter results by. Default (-1) is no radius.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Stop>>> GetStopsNearbyAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> limitModes, IEnumerable<string> servesLines, DateTime? at, double latitude, double longitude, bool showChildren = false, int radiusInMeters = -1, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetStops(tokenComponent, settings, ct, onlyAgencies, omitAgencies, limitModes, servesLines, at, latitude, longitude, string.Empty, showChildren, radiusInMeters, limit, offset);
        }

        /// <summary>
        /// Gets a list of all stops in the system.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="limitModes">Optional list of modes to filter the results by.</param>
        /// <param name="servesLines">Optional list of lines to filter the results by.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="showChildren">Optionally adds in the children stops for this call.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Stop>>> GetStopsAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> limitModes, IEnumerable<string> servesLines, DateTime? at, bool showChildren = false, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetStops(tokenComponent, settings, ct, onlyAgencies, omitAgencies, limitModes, servesLines, at, double.NaN, double.NaN, string.Empty, showChildren: showChildren, limit: limit, offset: offset);
        }

        /// <summary>
        /// Gets a list of all stops within a bounding box.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="limitModes">Optional list of modes to filter the results by.</param>
        /// <param name="servesLines">Optional list of lines to filter the results by.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="boundingBox">Required comma-separated SW (south west) latitude, SW longitude, NE (north east) latitude and NE longitude in that order. Example: -33.944,18.36,-33.895,18.43</param>
        /// <param name="showChildren">Optionally adds in the children stops for this call.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Stop>>> GetStopsByBoundingBoxAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> limitModes, IEnumerable<string> servesLines, DateTime? at, string boundingBox, bool showChildren = false, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetStops(tokenComponent, settings, ct, onlyAgencies, omitAgencies, limitModes, servesLines, at, double.NaN, double.NaN, boundingBox, showChildren: showChildren, limit: limit, offset: offset);
        }

        /// <summary>
        /// Gets the details of a Stop.
        /// </summary>
        /// <param name="id">Id of the Stop to get.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<Stop>> GetStopAsync(CancellationToken ct, string id, DateTime? at)
        {
            return await transitApiComponent.GetStop(tokenComponent, settings, ct, id, at);
        }

        /// <summary>
        /// Gets the children of a parent Stop.
        /// </summary>
        /// <param name="id">Id of the parent Stop to get children for.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<List<Stop>>> GetChildStopsAsync(CancellationToken ct, string id, DateTime? at)
        {
            return await transitApiComponent.GetChildStops(tokenComponent, settings, ct, id, at);
        }

        #endregion

        #region Lines

        /// <summary>
        /// Gets a list of all lines in the system.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="limitModes">Optional list of modes to filter the results by.</param>
        /// <param name="servesStops">Optional list of lines to filter the results by.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Line>>> GetLinesAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> limitModes, IEnumerable<string> servesStops, DateTime? at, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetLines(tokenComponent, settings, ct, onlyAgencies, omitAgencies, limitModes, servesStops, at, limit: limit, offset: offset);
        }

        /// <summary>
        /// Gets the details of a Stop.
        /// </summary>
        /// <param name="id">Id of the Stop to get.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<Line>> GetLineAsync(CancellationToken ct, string id, DateTime? at)
        {
            return await transitApiComponent.GetLine(tokenComponent, settings, ct, id, at);
        }

        #endregion

        #region Journeys

        /// <summary>
        /// Get a list of itineraties going from A to B.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="fareProducts">Optional list of fare products to use. If none are provided, the default fare product for an agency is used.</param>
        /// <param name="limitModes">Optional list of modes to filter the results by.</param>
        /// <param name="earliestDepartureTime">Optional earliest desired departure date and time for the journey.</param>
        /// <param name="latestArrivalTime">Optional latest desired arrival date and time for the journey.</param>
        /// <param name="maxItineraries">Optional maximum number of itineraries to return. Valid values: 1 - 5.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<Journey>> PostJourneyAsync(CancellationToken ct, IEnumerable<string> fareProducts, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> onlyModes, IEnumerable<TransportMode> omitModes, double startLatitude, double startLongitude, double endLatitude, double endLongitude, DateTime? time, TimeType timeType = TimeType.DepartAfter, Profile profile = Profile.ClosestToTime, int maxItineraries = 3)
        {
            return await transitApiComponent.PostJourney(tokenComponent, settings, ct, fareProducts, onlyAgencies, omitAgencies, onlyModes, omitModes, startLatitude, startLongitude, endLatitude, endLongitude, time, timeType, profile, maxItineraries);
        }

        #endregion

        #region Timetables

        /// <summary>
        /// Retrieves a timetable for a stop, consisting of a list of occurrences of a vehicle calling at this stop in order of arrival time.
        /// </summary>
        /// <param name="id">Id of the Stop Timetable to get.</param>
        /// <param name="earliestArrivalTime">Optional earliest desired arrival date and time to include in the timetable.</param>
        /// <param name="latestArrivalTime">Optional latest desired arrival date and time to include in the timetable. Defaults to earliestArrivalTime plus 7 days.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<StopTimetable>>> GetStopTimetableAsync(CancellationToken ct, string id, DateTime? earliestArrivalTime, DateTime? latestArrivalTime, DateTime? at, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetStopTimetable(tokenComponent, settings, ct, id, earliestArrivalTime, latestArrivalTime, at, limit: limit, offset: offset);
        }

        /// <summary>
        /// Retrieves a timetable for a stop, consisting of a list of occurrences of a vehicle calling at this stop in order of arrival time.
        /// </summary>
        /// <param name="id">Id of the Line Timetable to get.</param>
        /// <param name="departureStopIdFilter">Optional stop identifier - bounds results to only occur after this stop.</param>
        /// <param name="arrivalStopIdFilter">Optional stop identifier - bounds results to only occur before this stop.</param>
        /// <param name="earliestDepartureTime">Optional earliest desired departure date and time to include in the timetable.</param>
        /// <param name="latestDepartureTime">Optional latestet desired departure date and time to include in the timetable. Defaults to earliestDepartureTime plus 7 days.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<LineTimetable>>> GetLineTimetableAsync(CancellationToken ct, string id, string departureStopIdFilter, string arrivalStopIdFilter, DateTime? earliestDepartureTime, DateTime? latestDepartureTime, DateTime? at, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetLineTimetable(tokenComponent, settings, ct, id, departureStopIdFilter, arrivalStopIdFilter, earliestDepartureTime, latestDepartureTime, at, limit: limit, offset: offset);
        }

        #endregion

        #region Shapes

        /// <summary>
        /// Retrieves a shape for a line, consisting of an array of stop to stop segments that make up this line.
        /// </summary>
        /// <param name="id">Id of the Line.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<LineShape>>> GetLineShapeAsync(CancellationToken ct, string id, DateTime? at)
        {
            return await transitApiComponent.GetLineShape(tokenComponent, settings, ct, id, at);
        }

        #endregion

        #region Routes

        /// <summary>
        /// (EXPERIMENTAL FEATURE) Retrieves the routes for a line.
        /// </summary>
        /// <param name="experimentalPassPhrase">A unique key to gain access to this feature.</param>
        /// <param name="id">Id of the Line.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<Route>>> GetRoutesByLineAsync(CancellationToken ct, string experimentalPassPhrase, string id, DateTime? at)
        {
            return await transitApiComponent.GetRoutesByLine(tokenComponent, settings, ct, experimentalPassPhrase, id, at);
        }

        #endregion

        #region Fares

        /// <summary>
        /// Gets a list of fare products.
        /// </summary>
        /// <param name="onlyAgencies">Optional list of agencies to include in the results. (Cannot be specified if omitAgencies is provided)</param>
        /// <param name="omitAgencies">Optional list of agencies to exclude from the results. (Cannot be specified if onlyAgencies is provided)</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<FareProduct>>> GetFareProductsAsync(CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetFareProducts(tokenComponent, settings, ct, onlyAgencies, omitAgencies, at, limit, offset);
        }

        /// <summary>
        /// Gets a list of fare tables for a specific fare product.
        /// </summary>
        /// <param name="fareProductId">Id of the fare product to get fare tables for.</param>
        /// <param name="at">Optional point in time from which to query. Defaults to the current date and time.</param>
        /// <param name="limit">Optional maximum number of entities to be returned. Default is 100.</param>
        /// <param name="offset">Optional offset of the first entity returned. Default is 0.</param>
        /// <returns></returns>
        public async Task<TransportApiResult<IEnumerable<FareTable>>> GetFareTablesAsync(CancellationToken ct, string fareProductId, DateTime? at, int limit = 100, int offset = 0)
        {
            return await transitApiComponent.GetFareTables(tokenComponent, settings, ct, fareProductId, at, limit, offset);
        }

        #endregion
    }
}
