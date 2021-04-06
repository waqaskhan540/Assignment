using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Models
{
    public class RequestParams
    {
        public string Type { get; set; } = "koop";
        public string Zo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
