using Funda.Domain.Common.Models;
using Funda.Domain.Exceptions;
using Funda.Domain.Interfaces;
using Funda.Domain.Models;
using Funda.Domain.Services.Config;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Funda.Domain.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfig _apiConfig;
        private readonly IApiRateLimitMonitor _apiRateLimitMonitor;
        public ApiService(
            HttpClient httpClient,
            IOptions<ApiConfig> apiConfig,
            IApiRateLimitMonitor apiRateLimitMonitor)
        {
            _httpClient = httpClient;
            _apiRateLimitMonitor = apiRateLimitMonitor;
            _apiConfig = apiConfig.Value;
        }

        public async Task<PropertyListingResponse> GetPropertyListings(RequestParams parameters)
        {
            PropertyListingResponse result = null;
            try
            {
                //apiRateLimit monitor caches each request made to the API 
                //and keeps it only for one minute
                //checks if the number of requests exceed the max request count i.e. 100
                //if the number exceeds then throw an exception

                int requestCount = _apiRateLimitMonitor.GetRequestCount();
                if (requestCount >= _apiConfig.MaxRequestsPerMinute)
                    throw new ApiUnavailableException();

                //if the number of requests are less than 100 (or the limit defined)
                //then update the request counter 
                _apiRateLimitMonitor.UpdateRequestCount();

                //get feed URL and api key from config and make request to the API
                string uri = string.Format(_apiConfig.Feed, _apiConfig.ApiKey);
                
                //get query string parameters and create request url 
                string queryString = $"?type={parameters.Type}&zo={parameters.Zo}&page={parameters.Page}&pagesize={parameters.PageSize}";
                string RequestUri = $"{uri}/{queryString}";

                var response = await _httpClient.GetAsync(RequestUri);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<PropertyListingResponse>(jsonString);

            }            
            catch (Exception)
            {
                throw;
            }
            return result;

        }
    }
}
