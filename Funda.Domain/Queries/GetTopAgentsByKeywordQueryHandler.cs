using Funda.Domain.Common.Models;
using Funda.Domain.Dtos;
using Funda.Domain.Interfaces;
using Funda.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Domain.Queries
{
    public class GetTopAgentsByKeywordQueryHandler : IRequestHandler<GetTopAgentsByKeywordQuery, List<TopAgentDto>>
    {
        private readonly IApiService _apiService;

        public GetTopAgentsByKeywordQueryHandler(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<TopAgentDto>> Handle(GetTopAgentsByKeywordQuery request, CancellationToken cancellationToken)
        {
            var requestParams = new RequestParams
            {
                Zo = request.SearchQuery,
                Type = "koop"
            };

            var response = await _apiService.GetPropertyListings(requestParams);
            var properties = response.Properties;
            
            var topAgents = properties
                            .GroupBy(p => p.EstateAgentId)
                            .Select(x => new
                            {
                                EstateAgentId = x.Key,
                                EstateAgentName = x.First().EstateAgentName,
                                Properties = FilterByTagline(x,request.KeyWord),
                                PropertiesCount = FilterByTagline(x,request.KeyWord).Count
                            })
                            .OrderByDescending(x => x.PropertiesCount)
                            .Take(request.MaxResults)
                            .Select(x => new TopAgentDto
                            {
                                EstateAgentName = x.EstateAgentName,
                                PropertyCount = x.PropertiesCount
                            })
                            .Where(ea => ea.PropertyCount > 0)
                            .ToList();

            return topAgents;
        }

        private List<Property> FilterByTagline(IEnumerable<Property> properties,string searchTerm)
        {
            return properties
                    .Where(p => p.PromotionLabel.Tagline != null && 
                                p.PromotionLabel.Tagline.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
        }
    }
}
