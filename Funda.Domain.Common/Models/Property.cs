using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Funda.Domain.Common.Models
{
    public class Property
    {
        [JsonPropertyName("MakelaarNaam")]
        public string EstateAgentName { get; set; }
        [JsonPropertyName("MakelaarId")]
        public int EstateAgentId { get; set; }

        [JsonPropertyName("PromoLabel")]
        public PromoLabel PromotionLabel { get; set; }
    }
}
