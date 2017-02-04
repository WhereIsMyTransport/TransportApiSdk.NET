using System;
using TransportApi.Sdk.Components;
using TransportApi.Sdk;

namespace TransportApi.Sdk
{
    public class TransportApiClient : AbstractTransportApiClient
    {
        public TransportApiClient(TransportApiClientSettings settings)
            : base(settings, new TransportApiComponent(), new TokenComponent(settings.ClientId, settings.ClientSecret))
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "Settings cannot be null");
            }
        }
    }
}
