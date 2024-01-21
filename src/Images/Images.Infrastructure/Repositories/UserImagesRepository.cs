using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class UserImagesRepository(
        ImagesDbContext context,
        ILogger<UserImagesRepository> logger) : IUserImagesRepository
    {
        private readonly ImagesDbContext _context = context;
        private readonly ILogger<UserImagesRepository> _logger = logger;

        public async Task Add(string imageUrl, string userId)
        {
            try
            {
                _logger.LogInformation($"Attempting to add user image with userId: {userId} imageUrl: {imageUrl}");

                await _context.AdditionalUserData
                        .Where(addData => addData.UserId == userId)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(data => data.ImageURL, data => imageUrl));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. An error occured while trying to add new image to user with Id: {userId}.");
            }
        }

        public async Task Delete(string userId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete User image for user with id: {userId}");

                await _context.AdditionalUserData
                        .Where(addData => addData.UserId == userId)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(data => data.ImageURL, data => null));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. An error occured while trying to remove image for user with Id: {userId}");
            }
        }
    }
}
