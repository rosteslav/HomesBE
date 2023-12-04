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

        public async Task Add(string userId, PreferencesModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Purpose)
                && string.IsNullOrWhiteSpace(model.Region)
                && string.IsNullOrWhiteSpace(model.BuildingType)
                && model.PriceHigherEnd == 0)
            {
                _logger.LogInformation("DB user doesn't choose any preferences");
                return;
            }

            var preferences = _mapper.Map<Preferences>(model);
            preferences.UserId = userId;

            _logger.LogInformation("DB adding user preferences...");

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
