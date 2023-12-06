using AutoMapper;
using BuildingMarket.Auth.Application.Contracts;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Auth.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BuildingMarket.Auth.Infrastructure.Repositories
{
    public class PreferencesRepository(
        ApplicationDbContext context,
        IMapper mapper,
        ILogger<PreferencesRepository> logger)
        : IPreferencesRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<PreferencesRepository> _logger = logger;

        public async Task Add(PreferencesModel model)
        {
            _logger.LogInformation("DB adding user preferences...");
            var preferences = _mapper.Map<Preferences>(model);

            try
            {
                await _context.Preferences.AddAsync(preferences);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding user preferences");
            }
        }
    }
}
