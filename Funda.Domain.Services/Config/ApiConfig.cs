using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Services.Config
{
    public class ApiConfig
    {
        /// <summary>
        /// Base Uri for the API
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// API key to be used for authentication
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Max number of requests that can be made in one minute
        /// </summary>
        public int MaxRequestsPerMinute { get; set; }

        /// <summary>
        /// property listing endpoint
        /// </summary>
        public string Feed { get; set; }
    }
}
