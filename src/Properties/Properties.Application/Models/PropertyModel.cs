namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyModel : AddPropertyInputModel
    {
        public DateTime CreatedOnLocalTime { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}