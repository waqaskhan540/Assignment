using System.Text.Json.Serialization;

namespace Funda.Domain.Common.Models
{
    public class PromoLabel
    {
        [JsonPropertyName("HasPromotionLabel")]
        public bool HasPromotionLabel { get; set; }

        [JsonPropertyName("PromotionType")]
        public int PromotionType { get; set; }
        [JsonPropertyName("Tagline")]
        public string Tagline { get; set; }
    }
}
