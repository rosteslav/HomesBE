namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyOptionsWithFilterModel : PropertyOptionsModel
    {
        public IEnumerable<PublishedOnModel> PublishedOn { get; set; }
        public IEnumerable<string> OrderBy { get; set; }
    }
}
