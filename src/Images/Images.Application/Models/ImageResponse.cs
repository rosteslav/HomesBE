using System.Text.Json.Serialization;

namespace BuildingMarket.Images.Application.Models
{
    public class ImageResponse
    {

        [JsonPropertyName("data")]
        public ImageData Data { get; set; }
    }
}
