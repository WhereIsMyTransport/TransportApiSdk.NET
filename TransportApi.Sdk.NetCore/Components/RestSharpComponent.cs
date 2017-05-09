using RestSharp;
using System;
using TransportApi.Sdk.NetCore;

namespace TransportApi.Sdk.NetCore.Components
{
    internal static class RestSharpComponent
    {
        public static RestRequest GetRequest(string resource, string accessToken)
        {
            var request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            return request;
        }

        public static RestRequest PostRequest(string resource, string accessToken)
        {
            var request = new RestRequest(resource, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        public static RestClient Client(TimeSpan timeout)
        {
            var client = new RestClient(TransportApiClientConnection.TransportApiBaseUri);

            client.Timeout = (int)timeout.TotalMilliseconds;

            return client;
        }
    }
}
