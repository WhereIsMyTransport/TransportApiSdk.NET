using System;

namespace TransportApi.Sdk
{
    internal static class TransportApiClientConnection
    {
        public static Uri TransportApiBaseUri = new Uri("https://platform.whereismytransport.com/api");

        public static Uri IdentityStsTokenUri = new Uri("https://identity.whereismytransport.com/connect/token");
    }
}
