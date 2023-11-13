using AutoMapper;
using Demo.Application.Contracts;
using Demo.Domain.Enities;
using Demo.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Infrastucture.Repositories
{
    public class PropertyRepository(ILogger<PropertyRepository> logger, PropertyContext context, IMapper mapper) : IPropertyRepository
    {
        private readonly ILogger<PropertyRepository> _logger = logger;
        private readonly PropertyContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task Add(Property property)
        {
            _logger.LogInformation($"DB add property.\n " + property.ToString());

            try
            {
                await _context.Properties.AddAsync(property);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding property$ Type:{property.Type}");
            }
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"DB delete property with id: {id}");

            try
            {
                await _context.Properties.Where(i => i.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleteing property with id: {id}");
            }
        }

        public async Task Update(int id, Property property)
        {
            _logger.LogInformation($"DB update property with id: {id}");

            try
            {
                var propertyToEdit = await _context.Properties
                                    .Where(i => i.Id == id)
                                    .SingleOrDefaultAsync();
                //Fixed the statement to be true if the value of propertyToEdit is null
                if (propertyToEdit.Equals(null))
                {
                    _logger.LogInformation("No property with this ID is found.");
                }
                else
                {
                    propertyToEdit = _mapper.Map<Property>(propertyToEdit);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleteing property with ID: {id}");
            }
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            _logger.LogInformation("DB get all properties");

            try
            {
                var properties = await _context.Properties.ToListAsync();
                return properties;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all properties");
            }

            return Enumerable.Empty<Property>();
        }
    }
}
