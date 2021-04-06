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
    public class GetTopAgentsByPropertyCountQueryHandler : IRequestHandler<GetTopAgentsByPropertyCountQuery, List<TopAgentDto>>
    {
        private readonly IApiService _apiService;


        public GetTopAgentsByPropertyCountQueryHandler(IApiService messageService)
        {
            _apiService = messageService;
        }
        public async Task<List<TopAgentDto>> Handle(GetTopAgentsByPropertyCountQuery request, CancellationToken cancellationToken)
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
                                Properties = x.ToList(),
                                PropertiesCount = x.Count()
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
    }
}
