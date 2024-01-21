using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;
using BuildingMarket.Images.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class PropertyImagesRepository(
        ImagesDbContext context,
        ILogger<PropertyImagesRepository> logger)
        : IPropertyImagesRepository
    {
        private readonly ImagesDbContext _context = context;
        private readonly ILogger<PropertyImagesRepository> _logger = logger;

        public async Task Add(Image image)
        {
            try
            {
                _logger.LogInformation($"Attempting to add new Image with ImageURL: {image.ImageURL}");

                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. An error occured while trying to add new image with ImageURL: {image.ImageURL} to the database.");
            }
        }

        public async Task Delete(int imageId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete Image with Id: {imageId}");

                await _context.Images
                        .Where(i => i.Id == imageId)
                        .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. An error occured while trying to remove image with Id: {imageId} from the database.");
            }
        }

        public async Task<bool> Exists(int imageId)
        {
            try
            {
                _logger.LogInformation($"Checking if Image with Id: {imageId} exists.");

                return await _context.Images.AnyAsync(i => i.Id == imageId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. An error occured while trying to check if image with Id: {imageId} exists.");

                return false;
            }
        }

        public async Task<IEnumerable<PropertyImagesModel>> GetAllForAllProperties(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve all the images for all properties");

            try
            {
                var images = await _context.Images
                    .GroupBy(img => img.PropertyId)
                    .Select(pi => new PropertyImagesModel
                    {
                        PropertyId = pi.Key,
                        Images = pi.OrderBy(img => img.Id).Select(img => img.ImageURL)
                    })
                    .ToArrayAsync(cancellationToken);

                return images;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all the images for all properties");
            }

            return Enumerable.Empty<PropertyImagesModel>();
        }

        public async Task<IEnumerable<Image>> GetAllForProperty(int propertyId)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve all the images for property with id: {propertyId}");

                var imgs = await _context.Images
                    .Where(img => img.PropertyId == propertyId)
                    .OrderBy(img => img.Id)
                    .ToListAsync();

                return imgs;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Failed to retrieve all the images for property with id: {propertyId}");
                return Enumerable.Empty<Image>();
            }
        }

        public async Task<int> GetPropertyIdOfImageById(int imageId)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve image with Id: {imageId}");

                var img = await _context.Images
                    .FirstAsync(img => img.Id == imageId);

                return img.PropertyId;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Failed to retrieve image with Id: {imageId}");
                return default;
            }
        }
    }
}
