using System;

namespace TransportApi.Sdk
{
    public class TransportApiClientSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UniqueContextId { get; set; }

        public Uri EnvironmentUri { get; set; } = TransportApiClientConnection.TransportApiBaseUri;

        /// <summary>
        /// Additional scopes to request.
        /// Default is transportapi:all.
        /// </summary>
        public string ClientScopes { get; set; }

        /// <summary>
        /// Maximum time a request can run before timing out.
        /// Default is 30seconds.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        public TransportApiClientSettings()
        {
            Timeout = TimeSpan.FromSeconds(30);
            ClientScopes = "transportapi:all";
            UniqueContextId = Guid.NewGuid().ToString();
        }
    }
}