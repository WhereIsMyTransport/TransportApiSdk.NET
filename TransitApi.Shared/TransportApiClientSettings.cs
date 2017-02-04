using System;

namespace TransportApi.Sdk
{
    public class TransportApiClientSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        /// <summary>
        /// Maximum time a request can run before timing out.
        /// Default is 30seconds.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        public TransportApiClientSettings()
        {
            Timeout = TimeSpan.FromSeconds(30);
        }
    }
}
