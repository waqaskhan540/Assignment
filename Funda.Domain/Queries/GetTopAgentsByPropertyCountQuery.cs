using Funda.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Queries
{
    public class GetTopAgentsByPropertyCountQuery : IRequest<List<TopAgentDto>>
    {
        /// <summary>
        /// Maximum number of results to be returned
        /// </summary>
        public int MaxResults { get; set; }
        
        public string SearchQuery { get; set; }
    }
}
