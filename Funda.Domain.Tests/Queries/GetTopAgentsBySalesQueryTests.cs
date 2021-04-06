using Funda.Domain.Interfaces;
using Funda.Domain.Models;
using Funda.Domain.Queries;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Funda.Domain.Tests.Queries
{
    public class GetTopAgentsBySalesQueryTests
    {
        private Mock<IApiService> _apiService;
        public GetTopAgentsBySalesQueryTests()
        {
            _apiService = new Mock<IApiService>();
        }
        [Fact]
        public void Get_Top_Agents_With_Most_Listings_For_Sale()
        {

            _apiService
                    .Setup(x => x.GetPropertyListings(It.IsAny<RequestParams>()))
                    .Returns(Task.FromResult(MoqData.MoqListings));

            var sut = new GetTopAgentsByPropertyCountQueryHandler(_apiService.Object);

            var response = sut.Handle(
                new GetTopAgentsByPropertyCountQuery()
                {
                    MaxResults = 10,
                    SearchQuery = "any_query"
                }, CancellationToken.None).Result;

            var agent1PropertyCount = response.First(ea => ea.EstateAgentName == "agent1");
            var agent2PropertyCount = response.First(ea => ea.EstateAgentName == "agent2");
            var agent3PropertyCount = response.First(ea => ea.EstateAgentName == "agent3");
            var agent4PropertyCount = response.First(ea => ea.EstateAgentName == "agent4");

            Assert.Equal(3, agent1PropertyCount.PropertyCount);
            Assert.Equal(3, agent2PropertyCount.PropertyCount);
            Assert.Equal(2, agent3PropertyCount.PropertyCount);
            Assert.Equal(1, agent4PropertyCount.PropertyCount);

        }


    }
}
