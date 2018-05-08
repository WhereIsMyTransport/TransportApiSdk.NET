using System;
using TransportApi.Sdk.Components;
using TransportApi.Sdk.NetCore.Components;

namespace TransportApi.Sdk.NetCore
{
    public class TransportApiClient : AbstractTransportApiClient
    {
        public TransportApiClient(TransportApiClientSettings settings)
            : base(settings, new TransportApiComponent(), new TokenComponent(settings.ClientId, settings.ClientSecret, settings.ClientScopes))
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "Settings cannot be null");
            }
        }
    }
}
