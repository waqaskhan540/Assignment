using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Funda.Domain.Common.Models
{
    public class PropertyListingResponse
    {
        [JsonPropertyName("Objects")]
        public List<Property> Properties { get; set; }
    }
}
