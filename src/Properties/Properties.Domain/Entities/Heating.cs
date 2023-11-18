using BuildingMarket.Properties.Domain.Entities.Enums;

namespace BuildingMarket.Properties.Domain.Entities
{
    public class Heating
    {
        public int Id { get; set; }
        public HeatingType Type { get; set; }
    }
}
