﻿using RestSharp;
using System;
using System.Net;
using TransportApi.Sdk;

namespace TransportApi.Sdk.Components
{
    internal static class RestSharpComponent
    {
        public static RestRequest GetRequest(string resource, string accessToken, TransportApiClientSettings settings)
        {
            var request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Unique-Context-Id", settings.UniqueContextId);
            request.AddParameter("enable-experimental", settings.EnableExperimentalFeatures);

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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var client = new RestClient(platformUri);

            client.Timeout = (int)timeout.TotalMilliseconds;

            return client;
        }
    }
}