using Funda.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Queries
{
    public class GetTopAgentsByKeywordQuery : IRequest<List<TopAgentDto>>
    {
        /// <summary>
        /// They keyword to search for related properties         
        /// </summary>
        public string KeyWord { get; set; } 
        /// <summary>
        /// Maximum number of results to be returned
        /// </summary>
        public int MaxResults { get; set; }

        /// <summary>
        /// Search Query to be passed as querystring parameter to the API
        /// </summary>
        public string SearchQuery { get; set; } 
    }
}
