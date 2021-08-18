using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;
using CP380_B3_BlockBlazor.Data;


namespace CP380_B3_BlockBlazor.Data
{
    public class PendingTransactionService
    {
        static HttpClient _httpClient;
        private readonly IConfiguration _configure;
        private readonly JsonSerializerOptions _config = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        public PendingTransactionService(IHttpClientFactory HttpClientFactory, IConfiguration Configure)
        {
            _httpClient = HttpClientFactory.CreateClient();
            _configure = Configure.GetSection("PayloadService");
        }

        public async Task<IEnumerable<Payload>> GetPayloadsAsync()
        {
            var data = await _httpClient.GetAsync(_configure["url"]);

            if (data.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Payload>>(
                        await data.Content.ReadAsStreamAsync(), _config
                    );
            }

            return Array.Empty<Payload>();
        }

        public async Task<HttpResponseMessage> AddPayloadAsync(Payload payload)
        {
            var content = new StringContent(
                    JsonSerializer.Serialize(payload, _config),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    );
            var response = await _httpClient.PostAsync(_configure["url"], content);
            return response;
        }
    }
}
