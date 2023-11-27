using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class AdditionalUserDataRepository(
        ImagesDbContext context,
        ILogger<AdditionalUserDataRepository> logger) : IAdditionalUserDataRepository
    {
        private readonly ImagesDbContext _context = context;
        private readonly ILogger<AdditionalUserDataRepository> _logger = logger;

        public async Task AddUserImage(AdditionalUserData userData)
        {
            try 
            {
                _logger.LogInformation("Attempting to add user data.");

                var data = _context.AdditionalUserData.Where(x => x.UserId == userData.UserId).SingleOrDefault();

                if (data == null) 
                {
                    await _context.AdditionalUserData.AddAsync(new AdditionalUserData
                    {
                        FirstName = userData.FirstName,
                        LastName = userData.LastName,
                        PhoneNumber = userData.PhoneNumber,
                        UserId = userData.UserId,
                        ImageUrl = userData.ImageUrl,
                    });
                } 
                else
                {
                    _context.AdditionalUserData.Update(userData);
                }

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

                var user = _context.AdditionalUserData.Where(x => x.UserId == userId).SingleOrDefault();

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
