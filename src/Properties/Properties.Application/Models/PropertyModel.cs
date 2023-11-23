namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyModel : AddPropertyInputModel
    {
        public required PropertyOptionsModel PropertyOptions { get; set; }

        public DateTime CreatedOnLocalTime { get; set; }

        public ContactInfo ContactInfo { get; set; }
    }
}
