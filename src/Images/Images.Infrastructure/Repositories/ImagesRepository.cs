using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Domain.Entities;
using BuildingMarket.Images.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class ImagesRepository(
        ImagesDbContext context,
        ILogger<ImagesRepository> logger) : IImagesRepository
    {
        private readonly ImagesDbContext _context = context;
        private readonly ILogger<ImagesRepository> _logger = logger;

        public async Task AddPropertyImage(Image image)
        {
            try
            {
                _logger.LogInformation("Attempting to add new Image with ImageURL: {ImageURL}", image.ImageURL);

                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to add new image with ImageURL: {ImageURL} to the database.", ex.Message, image.ImageURL);
            }
        }

        public async Task AddUserImage(string imageUrl, string userId)
        {
            try
            {
                _logger.LogInformation("Attempting to add user image with userId: {userId} imageUrl: {imageUrl}", userId, imageUrl);

                await _context.AdditionalUserData
                        .Where(addData => addData.UserId == userId)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(data => data.ImageURL, data => imageUrl));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to add new image to user with Id: {userId}.", ex.Message, userId);
            }
        }

        public async Task DeletePropertyImage(int imageId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete Image with Id: {imageId}", imageId);

                await _context.Images
                        .Where(i => i.Id == imageId)
                        .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to remove image with Id: {imageId} from the database.", ex.Message, imageId);
            }
        }

        public async Task DeleteUserImage(string userId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete User image for user with id: {userId}", userId);

                await _context.AdditionalUserData
                        .Where(addData => addData.UserId == userId)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(data => data.ImageURL, data => null));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to remove image for user with Id: {userId}", ex.Message, userId);
            }
        }

        public async Task<bool> Exists(int imageId)
        {
            try
            {
                _logger.LogInformation("Checking if Image with Id: {imageId} exists.", imageId);

                return await _context.Images.AnyAsync(i => i.Id == imageId);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. An error occured while trying to check if image with Id: {imageId} exists.", ex.Message, imageId);

                return false;
            }
        }

        public async Task<IEnumerable<Image>> GetAllForProperty(int propertyId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all the images for property with id: {propertyId}", propertyId);

                var imgs = await _context.Images
                    .Where(img => img.PropertyId == propertyId)
                    .ToListAsync();

                return imgs;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. Failed to retrieve all the images for property with id: {propertyId}", ex.Message, propertyId);
                return Enumerable.Empty<Image>();
            }
        }

        public async Task<int> GetPropertyIdOfImageById(int imageId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve image with Id: {imageId}", imageId);

                var img = await _context.Images
                    .FirstAsync(img => img.Id == imageId);

                return img.PropertyId;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}. Failed to retrieve image with Id: {imageId}", ex.Message, imageId);
                return default;
            }
        }
    }
}
