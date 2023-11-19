using Newtonsoft.Json;

namespace BuildingMarket.Images.Application.Models
{
    public class ImageData
    {
        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("delete_url")]
        public string DeleteUrl { get; set; }
    }
}
