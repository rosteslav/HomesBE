using Demo.Application.Contracts;
using Demo.Domain.Enities;
using Demo.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Infrastucture.Repositories
{
    public class ItemRepository(ApplicationDbContext context, ILogger<ItemRepository> logger) : IItemRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<ItemRepository> _logger = logger;

        public async Task Add(Item item)
        {
            _logger.LogInformation($"DB add item: {item.Name}");

            try
            {
                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding item: {item.Name}");
            }
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"DB delete item with id: {id}");

            try
            {
                await _context.Items.Where(i => i.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleteing item with id: {id}");
            }
        }

        public async Task<IEnumerable<Item>> Get()
        {
            _logger.LogInformation("DB get all items");

            try
            {
                var items = await _context.Items.ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all items");
            }

            return Enumerable.Empty<Item>();
        }
    }
}
