using RestSharp;
using System;
using TransportApi.Sdk.NetCore;

namespace TransportApi.Sdk.NetCore.Components
{
    internal static class RestSharpComponent
    {
        public static RestRequest GetRequest(string resource, string accessToken, string uniqueContextId)
        {
            var request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Unique-Context-Id", uniqueContextId);

            return request;
        }

        public static RestRequest PostRequest(string resource, string accessToken, string uniqueContextId)
        {
            var request = new RestRequest(resource, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Unique-Context-Id", uniqueContextId);

            return request;
        }

        public static RestClient Client(TimeSpan timeout, Uri platformUri)
        {
            var client = new RestClient(platformUri);

            client.Timeout = (int)timeout.TotalMilliseconds;

            return client;
        }
    }
}
