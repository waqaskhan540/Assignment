using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Exceptions
{
    public class ApiUnavailableException : Exception
    {
        public ApiUnavailableException()
            : base("service unavailable due to too many requests, please try again in a minute") { }
        
    }
}
