using Funda.Domain.Common;
using Funda.Domain.Common.Models;
using Funda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funda.Domain.Interfaces
{
    public interface IApiService
    {
        Task<PropertyListingResponse> GetPropertyListings(RequestParams queryStringParams);
    }
}
