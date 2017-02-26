using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk.Models.Enums;
using TransportApi.Sdk.Models.ResultModels;

namespace TransportApi.Sdk.Interfaces
{
    public interface ITransportApiComponent
    {
        Task<TransportApiResult<Journey>> PostJourney(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> fareProducts, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> onlyModes, IEnumerable<TransportMode> omitModes, double startLatitude, double startLongitude, double endLatitude, double endLongitude, DateTime? time, TimeType timeType = TimeType.DepartAfter, Profile profile = Profile.ClosestToTime, int maxItineraries = 3, string exclude = null);

        Task<TransportApiResult<Journey>> GetJourney(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<Itinerary>> GetJourneyItinerary(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string journeyId, string itineraryId, DateTime? at, string exclude = null);

        Task<TransportApiResult<IEnumerable<Agency>>> GetAgencies(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, double latitude, double longitude, string boundingBox, int radiusInMeters = -1, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<Agency>> GetAgency(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<IEnumerable<Stop>>> GetStops(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> onlyModes, IEnumerable<string> servesLines, DateTime? at, double latitude, double longitude, string boundingBox, bool showChildren = false, int radiusInMeters = -1, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<Stop>> GetStop(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<List<Stop>>> GetChildStops(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<IEnumerable<Line>>> GetLines(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> onlyModes, IEnumerable<string> servesStops, DateTime? at, string boundingBox, double latitude, double longitude, int radiusInMeters = -1, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<Line>> GetLine(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<IEnumerable<StopTimetable>>> GetStopTimetable(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? earliestArrivalTime, DateTime? latestArrivalTime, DateTime? at, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<IEnumerable<LineTimetable>>> GetLineTimetable(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, string departureStopIdFilter, string arrivalStopIdFilter, DateTime? earliestDepartureTime, DateTime? latestDepartureTime, DateTime? at, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<IEnumerable<LineShape>>> GetLineShape(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<IEnumerable<Route>>> GetRoutesByLine(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null);

        Task<TransportApiResult<IEnumerable<FareTable>>> GetFareTables(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<IEnumerable<FareProduct>>> GetFareProducts(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, int limit = 100, int offset = 0, string exclude = null);

        Task<TransportApiResult<FareProduct>> GetFareProduct(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude);
    }
}
