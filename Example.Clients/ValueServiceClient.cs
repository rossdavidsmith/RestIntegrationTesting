using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Clients
{
    public class ValueServiceClient
    {
        private const string RootResourceName = "api/values/";

        private readonly HttpClient _httpClient;
        private readonly Uri _serviceBaseUri;

        public ValueServiceClient(
            HttpClient httpClient,
            Uri serverBaseUri)
        {
            _httpClient = httpClient;

            _serviceBaseUri = new Uri(serverBaseUri, RootResourceName);
        }

        public async Task<IReadOnlyList<string>> GetValuesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(_serviceBaseUri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Unexpected HTTP Status code: {response.StatusCode}");
            }

            return await response.Content.ReadAsAsync<IReadOnlyList<string>>(cancellationToken);
        }
    }
}
