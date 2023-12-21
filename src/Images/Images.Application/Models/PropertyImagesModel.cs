namespace BuildingMarket.Images.Application.Models
{
    public class PropertyImagesModel
    {
        public int PropertyId { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
