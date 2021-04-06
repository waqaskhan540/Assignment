using Funda.Domain.Exceptions;
using Funda.Domain.Interfaces;
using Funda.Domain.Services;
using Funda.Domain.Services.Config;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Funda.Domain.Tests.Services
{
    public class ApiServiceTests : IDisposable
    {
                
        private WireMockServer _server;      
        private Mock<IApiRateLimitMonitor> _apiRateLimitMonitor;
        private ApiConfig _apiConfig;
        public ApiServiceTests()
        {            
            _apiRateLimitMonitor = new Mock<IApiRateLimitMonitor>();           
            _server = WireMockServer.Start();
            _apiConfig = new ApiConfig
            {
                ApiKey = "fake_key",
                BaseUri = _server.Urls[0],
                Feed = "/feeds/Aanbod.svc/json/{0}",
                MaxRequestsPerMinute = 100
            };

        }

       

        [Fact]
        public async Task Should_Return_Valid_Listings_If_API_Returns_200()
        {
            _server
                  .Given(Request.Create().WithPath("/feeds/Aanbod.svc/json/*").UsingGet())
                  .RespondWith(
                    Response.Create()
                      .WithStatusCode(200)
                     .WithBodyFromFile("Services/moq_api_response.json")
                  );

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_apiConfig.BaseUri)
            };

            var options = new Mock<IOptions<ApiConfig>>();
            options.Setup(x => x.Value).Returns(_apiConfig);

            _apiRateLimitMonitor.Setup(x => x.GetRequestCount()).Returns(0);

            var sut = new ApiService(httpClient, options.Object, _apiRateLimitMonitor.Object);
            var response = await sut.GetPropertyListings(new Models.RequestParams { Type = "koop",Zo ="/amsterdam/tuin" });

            Assert.NotNull(response);
            Assert.True(response.Properties.Count > 0);
            _apiRateLimitMonitor.Verify(m => m.UpdateRequestCount(), Times.Once);
        }

        [Fact]
        public void  Should_Throw_Exception_If_API_Returns_Non_Success_StatusCode()
        {
            _server
                  .Given(Request.Create().WithPath("/feeds/Aanbod.svc/json/*").UsingGet())
                  .RespondWith(
                    Response.Create()
                      .WithStatusCode(500)                     
                  );

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_apiConfig.BaseUri)
            };

            var options = new Mock<IOptions<ApiConfig>>();
            options.Setup(x => x.Value).Returns(_apiConfig);

            _apiRateLimitMonitor.Setup(x => x.GetRequestCount()).Returns(0);

            var sut = new ApiService(httpClient, options.Object, _apiRateLimitMonitor.Object);
             Assert.ThrowsAny<Exception>(() => sut.GetPropertyListings(new Models.RequestParams { Type = "koop", Zo = "/amsterdam/tuin" }).Result);
            
        }

        [Fact]
        public void Throw_Exception_If_Api_Requests_Exceed_Limit()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_apiConfig.BaseUri)
            };

            var options = new Mock<IOptions<ApiConfig>>();
            options.Setup(x => x.Value).Returns(_apiConfig);

            _apiRateLimitMonitor.Setup(x => x.GetRequestCount()).Returns(100);

            var sut = new ApiService(httpClient, options.Object, _apiRateLimitMonitor.Object);
            Assert.ThrowsAsync<ApiUnavailableException>(() => sut.GetPropertyListings(new Models.RequestParams()));
        }

        public void Dispose()
        {
            _server.Stop();
        }

    }
}
