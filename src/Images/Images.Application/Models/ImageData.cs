using System.Text.Json.Serialization;

namespace BuildingMarket.Images.Application.Models
{
    public class ImageData
    {
        [JsonPropertyName("display_url")]
        public string DisplayUrl { get; set; }
    }
}
