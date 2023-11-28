using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class AdditionalUserDataRepository(
        ImagesDbContext context,
        ILogger<AdditionalUserDataRepository> logger) : IAdditionalUserDataRepository
    {
        private readonly ImagesDbContext _context = context;
        private readonly ILogger<AdditionalUserDataRepository> _logger = logger;

        public async Task AddUserImage(string userId, string imageUrl)
        {
            try 
            {
                _logger.LogInformation("Attempting to add user data.");

                var data = await _context.AdditionalUserData
                    .Where(x => x.UserId == userId)
                    .FirstAsync();

                data.ImageUrl = imageUrl;

                _context.AdditionalUserData.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to add new user data to the database.", ex.Message);
            }
        }

        public async Task DeleteUserImage(string userId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete user image.");

                var user = await _context.AdditionalUserData
                    .Where(x => x.UserId == userId)
                    .FirstAsync();

                user.ImageUrl = null;

                _context.AdditionalUserData.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to add new user data to the database.", ex.Message);
            }
        }
    }
}
