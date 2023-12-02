using BuildingMarket.Properties.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Properties.Application.Models
{
    public class PropertyProjectToModel
    {
        public Property Property { get; set; }
        public IdentityUser User { get; set; }
        public AdditionalUserData UserData { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
