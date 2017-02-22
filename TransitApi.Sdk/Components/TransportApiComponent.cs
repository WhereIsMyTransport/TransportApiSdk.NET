using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransportApi.Sdk.Interfaces;
using TransportApi.Sdk.Models.Enums;
using TransportApi.Sdk.Models.InputModels;
using TransportApi.Sdk.Models.ResultModels;
using static TransportApi.Sdk.Components.RestSharpComponent;

namespace TransportApi.Sdk.Components
{
    internal sealed class TransportApiComponent : ITransportApiComponent
    {
        private static int maxLimit = 30000;

        public async Task<TransportApiResult<Journey>> PostJourney(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> fareProducts, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> onlyModes, IEnumerable<TransportMode> omitModes, double startLatitude, double startLongitude, double endLatitude, double endLongitude, DateTime? time, TimeType timeType = TimeType.DepartAfter, Profile profile = Profile.ClosestToTime, int maxItineraries = 3, string exclude = null)
        {
            var result = new TransportApiResult<Journey>();

            if (maxItineraries < 1 || maxItineraries > 5)
            {
                result.Error = "Invalid value for maxItineraries. Expected a value between or including 1 and 5.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            string timeIso8601 = null;
            if (time != null)
            {
                timeIso8601 = time.Value.ToString("o");
            }

            JourneyInputModel jsonBody = new JourneyInputModel()
            {
                Time = timeIso8601,
                TimeType = timeType.ToString(),
                FareProducts = fareProducts,
                MaxItineraries = maxItineraries,
                Profile = profile.ToString(),
                Omit = new JourneyOmitInputModel()
                {
                    Agencies = omitAgencies,
                    Modes = omitModes
                },
                Only = new JourneyOnlyInputModel()
                {
                    Agencies = onlyAgencies,
                    Modes = onlyModes
                },
                Geometry = new GeoJsonLineString()
                {
                    Type = GeoJsonType.MultiPoint,
                    Coordinates = new List<List<string>>()
                    {
                        new List<string>() { startLongitude.ToString(CultureInfo.InvariantCulture), startLatitude.ToString(CultureInfo.InvariantCulture) },
                        new List<string>() { endLongitude.ToString(CultureInfo.InvariantCulture), endLatitude.ToString(CultureInfo.InvariantCulture) }
                    }
                }
            };

            var client = Client(settings.Timeout);

            string path = "journeys";

            if (!string.IsNullOrWhiteSpace(exclude))
            {
                path += "?exclude=" + exclude;
            }

            var request = PostRequest(path, token);

            request.AddJsonBody(jsonBody);

            try
            {
                IRestResponse<Journey> restResponse = await client.ExecuteTaskAsync<Journey>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<Journey>> GetJourney(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<Journey>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Journey Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"journeys/{id}", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<Journey> restResponse = await client.ExecuteTaskAsync<Journey>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<Itinerary>> GetJourneyItinerary(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string journeyId, string itineraryId, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<Itinerary>();

            if (string.IsNullOrWhiteSpace(journeyId))
            {
                result.Error = "Journey Id is required.";

                return result;
            }

            if (string.IsNullOrWhiteSpace(itineraryId))
            {
                result.Error = "Itinerary Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"journeys/{journeyId}/itineraries/{itineraryId}", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<Itinerary> restResponse = await client.ExecuteTaskAsync<Itinerary>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<Agency>>> GetAgencies(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, double latitude, double longitude, string boundingBox, int radiusInMeters = -1, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<Agency>>();

            if (radiusInMeters < -1)
            {
                result.Error = "Invalid radius. Valid values are positive numbers or -1.";

                return result;
            }
            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }
            if (onlyAgencies != null && omitAgencies != null && onlyAgencies.Any() && omitAgencies.Any())
            {
                result.Error = "Either onlyAgencies or omitAgencies can be provided. Not both.";

                return result;
            }
            if (!string.IsNullOrWhiteSpace(boundingBox))
            {
                if (boundingBox.Split(',').Count() != 4)
                {
                    result.Error = "Invalid bounding box. See valid examples here: http://developer.whereismytransport.com/documentation#bounding-box.";

                    return result;
                }
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest("agencies", token);

            if (omitAgencies != null && omitAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", omitAgencies.Select(r => string.Concat('~', r))));
            }
            if (onlyAgencies != null && onlyAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", onlyAgencies));
            }
            if (!double.IsNaN(latitude) && !double.IsNaN(longitude))
            {
                request.AddParameter("point", latitude.ToString(CultureInfo.InvariantCulture) + "," + longitude.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(boundingBox))
            {
                request.AddParameter("bbox", boundingBox);
            }
            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (radiusInMeters > 0)
            {
                request.AddParameter("radius", radiusInMeters.ToString(CultureInfo.InvariantCulture));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<Agency>> restResponse = await client.ExecuteTaskAsync<List<Agency>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<Agency>> GetAgency(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<Agency>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Agency Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"agencies/{id}", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<Agency> restResponse = await client.ExecuteTaskAsync<Agency>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<Stop>>> GetStops(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> onlyModes, IEnumerable<string> servesLines, DateTime? at, double latitude, double longitude, string boundingBox, bool showChildren = false, int radiusInMeters = -1, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<Stop>>();

            if (radiusInMeters < -1)
            {
                result.Error = "Invalid radius. Valid values are positive numbers or -1.";

                return result;
            }
            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }
            if (onlyAgencies != null && omitAgencies != null && onlyAgencies.Any() && omitAgencies.Any())
            {
                result.Error = "Either onlyAgencies or omitAgencies can be provided. Not both.";

                return result;
            }
            if (!string.IsNullOrWhiteSpace(boundingBox))
            {
                if (boundingBox.Split(',').Count() != 4)
                {
                    result.Error = "Invalid bounding box. See valid examples here: http://developer.whereismytransport.com/documentation#bounding-box.";

                    return result;
                }
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest("stops", token);

            if (omitAgencies != null && omitAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", omitAgencies.Select(r => string.Concat('~', r))));
            }
            if (onlyAgencies != null && onlyAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", onlyAgencies));
            }
            if (onlyModes != null && onlyModes.Any())
            {
                request.AddParameter("modes", string.Join(",", onlyModes));
            }
            if (servesLines != null && servesLines.Any())
            {
                request.AddParameter("servesLines", string.Join(",", servesLines));
            }
            if (!double.IsNaN(latitude) && !double.IsNaN(longitude))
            {
                request.AddParameter("point", latitude.ToString(CultureInfo.InvariantCulture) + "," + longitude.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(boundingBox))
            {
                request.AddParameter("bbox", boundingBox);
            }
            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (radiusInMeters > 0)
            {
                request.AddParameter("radius", radiusInMeters.ToString(CultureInfo.InvariantCulture));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (showChildren)
            {
                request.AddParameter("showChildren", "true");
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<Stop>> restResponse = await client.ExecuteTaskAsync<List<Stop>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<Stop>> GetStop(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<Stop>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Stop Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"stops/{id}", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<Stop> restResponse = await client.ExecuteTaskAsync<Stop>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<List<Stop>>> GetChildStops(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<List<Stop>>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Stop Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"stops/{id}/stops", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<Stop>> restResponse = await client.ExecuteTaskAsync<List<Stop>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<Line>>> GetLines(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, IEnumerable<TransportMode> limitModes, IEnumerable<string> servesStops, DateTime? at, string boundingBox, double latitude, double longitude, int radiusInMeters = -1, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<Line>>();

            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }
            if (onlyAgencies != null && omitAgencies != null && onlyAgencies.Any() && omitAgencies.Any())
            {
                result.Error = "Either onlyAgencies or omitAgencies can be provided. Not both.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest("lines", token);

            if (omitAgencies != null && omitAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", omitAgencies.Select(r => string.Concat('~', r))));
            }
            if (onlyAgencies != null && onlyAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", onlyAgencies));
            }
            if (limitModes != null && limitModes.Any())
            {
                request.AddParameter("modes", string.Join(",", limitModes));
            }
            if (servesStops != null && servesStops.Any())
            {
                request.AddParameter("servesStops", string.Join(",", servesStops));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (!double.IsNaN(latitude) && !double.IsNaN(longitude))
            {
                request.AddParameter("point", latitude.ToString(CultureInfo.InvariantCulture) + "," + longitude.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(boundingBox))
            {
                request.AddParameter("bbox", boundingBox);
            }
            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (radiusInMeters > 0)
            {
                request.AddParameter("radius", radiusInMeters.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<Line>> restResponse = await client.ExecuteTaskAsync<List<Line>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<Line>> GetLine(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<Line>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Line Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"lines/{id}", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<Line> restResponse = await client.ExecuteTaskAsync<Line>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<StopTimetable>>> GetStopTimetable(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? earliestArrivalTime, DateTime? latestArrivalTime, DateTime? at, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<StopTimetable>>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Stop Id is required.";

                return result;
            }
            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"stops/{id}/timetables", token);

            if (earliestArrivalTime != null)
            {
                request.AddParameter("earliestArrivalTime", earliestArrivalTime.Value.ToString("o"));
            }
            if (latestArrivalTime != null)
            {
                request.AddParameter("latestArrivalTime", latestArrivalTime.Value.ToString("o"));
            }
            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<StopTimetable>> restResponse = await client.ExecuteTaskAsync<List<StopTimetable>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<LineTimetable>>> GetLineTimetable(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, string departureStopIdFilter, string arrivalStopIdFilter, DateTime? earliestDepartureTime, DateTime? latestDepartureTime, DateTime? at, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<LineTimetable>>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Line Id is required.";

                return result;
            }
            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"lines/{id}/timetables", token);

            if (earliestDepartureTime != null)
            {
                request.AddParameter("earliestDepartureTime", earliestDepartureTime.Value.ToString("o"));
            }
            if (latestDepartureTime != null)
            {
                request.AddParameter("latestDepartureTime", latestDepartureTime.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(departureStopIdFilter))
            {
                request.AddParameter("departureStopId", departureStopIdFilter);
            }
            if (!string.IsNullOrWhiteSpace(arrivalStopIdFilter))
            {
                request.AddParameter("arrivalStopId", arrivalStopIdFilter);
            }
            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<LineTimetable>> restResponse = await client.ExecuteTaskAsync<List<LineTimetable>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<LineShape>>> GetLineShape(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<LineShape>>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Line Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"lines/{id}/shape", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<LineShape>> restResponse = await client.ExecuteTaskAsync<List<LineShape>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<Route>>> GetRoutesByLine(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<Route>>();

            if (string.IsNullOrWhiteSpace(id))
            {
                result.Error = "Line Id is required.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"lines/{id}/routes", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<Route>> restResponse = await client.ExecuteTaskAsync<List<Route>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<FareProduct>>> GetFareProducts(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, IEnumerable<string> onlyAgencies, IEnumerable<string> omitAgencies, DateTime? at, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<FareProduct>>();

            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }
            if (onlyAgencies != null && omitAgencies != null && onlyAgencies.Any() && omitAgencies.Any())
            {
                result.Error = "Either onlyAgencies or omitAgencies can be provided. Not both.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest("fareproducts", token);

            if (omitAgencies != null && omitAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", omitAgencies.Select(r => string.Concat('~', r))));
            }
            if (onlyAgencies != null && onlyAgencies.Any())
            {
                request.AddParameter("agencies", string.Join(",", onlyAgencies));
            }
            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<FareProduct>> restResponse = await client.ExecuteTaskAsync<List<FareProduct>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<TransportApiResult<IEnumerable<FareTable>>> GetFareTables(ITokenComponent tokenComponent, TransportApiClientSettings settings, CancellationToken ct, string id, DateTime? at, int limit = 100, int offset = 0, string exclude = null)
        {
            var result = new TransportApiResult<IEnumerable<FareTable>>();

            if (limit > maxLimit || limit < 0)
            {
                result.Error = $"Invalid limit. Valid values are positive numbers up to {maxLimit}.";

                return result;
            }

            var token = await tokenComponent.GetAccessToken();

            if (token == null)
            {
                result.Error = tokenComponent.DefaultErrorMessage;

                return result;
            }

            var client = Client(settings.Timeout);

            var request = GetRequest($"fareproducts/{id}/faretables", token);

            if (at != null)
            {
                request.AddParameter("at", at.Value.ToString("o"));
            }
            if (limit != 100)
            {
                request.AddParameter("limit", limit.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                request.AddParameter("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrWhiteSpace(exclude))
            {
                request.AddParameter("exclude", exclude);
            }

            try
            {
                IRestResponse<List<FareTable>> restResponse = await client.ExecuteTaskAsync<List<FareTable>>(request, ct);

                result.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.IsSuccess = true;
                    result.Data = restResponse.Data;
                }
                else
                {
                    result.Error = ((RestResponseBase)restResponse).Content;
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

    }
}
