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

        public async Task Add(Image image)
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

        public async Task Delete(int imageId)
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
    }
}
