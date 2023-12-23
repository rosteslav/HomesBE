using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyDetailsWithImagesModel
    {
        public Property Property { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
