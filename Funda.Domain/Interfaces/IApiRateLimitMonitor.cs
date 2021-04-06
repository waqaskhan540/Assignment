using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Interfaces
{
    public interface IApiRateLimitMonitor
    {
        int GetRequestCount();
        void UpdateRequestCount();
    }
}
