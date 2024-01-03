using System.Text.Json.Serialization;

namespace BuildingMarket.Admins.Application.Models
{
    public class NeighbourhoodsRatingModel
    {
        [JsonPropertyName("for_living")]
        public IEnumerable<IEnumerable<string>> ForLiving { get; set; } = new List<string[]>();

        [JsonPropertyName("for_investment")]
        public IEnumerable<IEnumerable<string>> ForInvestment { get; set; } = new List<string[]>();

        public IEnumerable<IEnumerable<string>> Budget { get; set; } = new List<string[]>();

        public IEnumerable<IEnumerable<string>> Luxury { get; set; } = new List<string[]>();
    }
}
