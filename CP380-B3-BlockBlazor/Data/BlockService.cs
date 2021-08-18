using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;

namespace CP380_B3_BlockBlazor.Data
{
    public class BlockService
    {
        static HttpClient _httpClient;
        private readonly IConfiguration _configure;

        public BlockService(IHttpClientFactory HttpClientFactory, IConfiguration Configure)
        {
            _httpClient = HttpClientFactory.CreateClient();
            _configure = Configure.GetSection("BlockService");
        }

        public async Task<IEnumerable<Block>> GetBlocksAsync()
        {
            var data = await _httpClient.GetAsync(_configure["url"]);

            if (data.IsSuccessStatusCode)
            {
                JsonSerializerOptions config = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                return await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(
                        await data.Content.ReadAsStreamAsync(), config
                    );
            }

            return Array.Empty<Block>();
        }
    }
}
