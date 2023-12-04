using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class AdditionalUserDataRepository(ILogger<AdditionalUserDataRepository> logger, ApplicationDbContext context)
        : IAdditionalUserDataRepository
    {
        private readonly ILogger<AdditionalUserDataRepository> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(AdditionalUserData item)
        {
            try
            {
                _logger.LogInformation($"Trying to add Additional User Data with data: First Name:{item.FirstName ?? "None"}, Last Name:{item.LastName ?? "None"} Phone Number:{item.PhoneNumber ?? "None"}");
                await _context.AdditionalUserData.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Couldn't add Additional User Data! {item.FirstName} {item.LastName} {item.PhoneNumber}.");
            }
        }

        public async Task<AdditionalUserData> GetById(string userId)
            => await _context.AdditionalUserData
                .FirstAsync(u => u.UserId == userId);
    }
}
