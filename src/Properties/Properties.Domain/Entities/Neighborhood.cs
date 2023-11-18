using BuildingMarket.Properties.Domain.Entities.Enums;

namespace BuildingMarket.Properties.Domain.Entities
{
    public class Neighborhood
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }
    }
}
