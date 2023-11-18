using BuildingMarket.Properties.Domain.Entities.Enums;

namespace BuildingMarket.Properties.Domain.Entities
{ 
    public class BuildingType
    {
        public int Id { get; set; }

        public BuildingMaterial Type { get; set; }
    }
}
