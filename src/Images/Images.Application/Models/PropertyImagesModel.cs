namespace BuildingMarket.Images.Application.Models
{
    public class PropertyImagesModel
    {
        public int PropertyId { get; set; }
        public IEnumerable<ImagesResult> Images { get; set; }
    }
}
