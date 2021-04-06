using Funda.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funda.Api.Controllers
{
    [Route("api/agents/top")]
    [ApiController]
    public class TopAgentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TopAgentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("by-property-count")]
        public async Task<IActionResult> GetTopAgentsByPropertyCount()
        {
            //hard-coding parameters for brevity , but could be made dynamic by receiving from 
            //client side

            var response = await _mediator.Send(new GetTopAgentsByPropertyCountQuery()
            {                
                MaxResults = 10,                
                SearchQuery = "/amsterdam/tuin"
            });

            return Ok(response);
        }

        [HttpGet("by-property-keyword")]
        public async Task<IActionResult> GetTopAgentsByKeyword()
        {
            //hard-coding parameters for brevity , but could be made dynamic by receiving from 
            //client side

            var response = await _mediator.Send(new GetTopAgentsByKeywordQuery()
            {
                MaxResults = 10,
                KeyWord = "tuin", // filter properties containing keyword 'tuin'
                SearchQuery = "/amsterdam/tuin"
            });
            return Ok(response);

        }
    }
}
