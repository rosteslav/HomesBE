namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyOptionsWithFilterModel : PropertyOptionsModel
    {
        public IEnumerable<PublishedOnModel> PublishedOn { get; set; }
        public IEnumerable<OrderByModel> OrderBy { get; set; }
    }
}
