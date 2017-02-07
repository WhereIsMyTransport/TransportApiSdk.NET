# TransportApiSdk - WhereIsMyTransport API Client Library for .NET

The unofficial .NET SDK for the [WhereIsMyTransport](https://www.whereismytransport.com) API. 

Access to the platform is completely free, so for more information and to get credentials, just visit the [developer portal](https://developer.whereismytransport.com).

## Usage

```c#
using TransportApi.Sdk;

// Define the api client with your credentials.
TransportApiClient defaultClient = new TransportApiClient(new TransportApiClientSettings()
        {
            ClientId = "YOUR_CLIENT_ID",
            ClientSecret = "YOUR_CLIENT_SECRET"
        });

// Make an api call.
var results = await defaultClient.GetAgenciesAsync(CancellationToken.None, null, null, DateTime.UtcNow);

// Do fancy things with the results.
```

## Installation
TransportApiSdk.NET is available on NuGet.

```
Install-Package TransportApi.Sdk
```
Also available for mobile devices.

```
Install-Package TransportApi.Portable.Sdk
```

## Author

Chris King - https://twitter.com/crkingza

## License

TransportApiSdk is available under the MIT license. See the LICENSE file for more info.
