using BuildingMarket.Auth.Application.Models.AuthOptions;
using MediatR;

namespace BuildingMarket.Auth.Application.Features.AuthOptions.Queries.GetPreferences
{
    public class GetPreferencesQuery : IRequest<PreferencesOutputModel>
    {
    }
}
