using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class PropertiesService(ImagesDbContext context)
        : IPropertiesService
    {
        private readonly ImagesDbContext _context = context;

        public async Task<bool> IsPropertyOwner(int propertyId, string userId)
            => await _context.Properties
                .AnyAsync(p => p.Id == propertyId && p.SellerId == userId);

        public async Task<bool> PropertyExists(int propertyId)
            => await _context.Properties.AnyAsync(p => p.Id == propertyId);
    }
}
