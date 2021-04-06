using Funda.Domain.Interfaces;
using Funda.Domain.Models;
using Funda.Domain.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Funda.Domain.Tests.Queries
{
    public class GetTopAgentsByPropertyTypeTests
    {
        private Mock<IApiService> _apiService;
        public GetTopAgentsByPropertyTypeTests()
        {
            _apiService = new Mock<IApiService>();
        }


        [Fact]
        public void Get_Top_Agents_With_Listings_With_Keyword()
        {
            _apiService
                    .Setup(x => x.GetPropertyListings(It.IsAny<RequestParams>()).Result)
                    .Returns(MoqData.MoqListings);

            var sut = new GetTopAgentsByKeywordQueryHandler(_apiService.Object);
            var response = sut.Handle(
                    new GetTopAgentsByKeywordQuery
                    {
                        KeyWord = "garden",
                        SearchQuery = "anykeyword",
                        MaxResults = 10
                    },
                    CancellationToken.None).Result;


            var agent2PropertyCount = response.First(ea => ea.EstateAgentName == "agent2");
            var agent3PropertyCount = response.First(ea => ea.EstateAgentName == "agent3");
            var agent4PropertyCount = response.First(ea => ea.EstateAgentName == "agent4");

            Assert.Equal(1, agent2PropertyCount.PropertyCount);
            Assert.Equal(2, agent3PropertyCount.PropertyCount);
            Assert.Equal(1, agent4PropertyCount.PropertyCount);
        }
    }
}
