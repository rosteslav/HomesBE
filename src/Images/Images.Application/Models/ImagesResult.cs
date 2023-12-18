using MessagePack;

namespace BuildingMarket.Images.Application.Models
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ImagesResult
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
    }
}
