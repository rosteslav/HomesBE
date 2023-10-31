using Demo.Application.Contracts;
using Demo.Domain.Enities;
using Demo.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Infrastucture.Repositories
{
    public class ItemRepository(ILogger<ItemRepository> logger, ItemContext context) : IItemRepository
    {
        private readonly ILogger<ItemRepository> _logger = logger;
        private readonly ItemContext _context = context;

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
