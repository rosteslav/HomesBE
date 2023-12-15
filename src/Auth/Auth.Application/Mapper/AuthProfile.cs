using AutoMapper;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Domain.Entities;

namespace BuildingMarket.Auth.Application.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<PreferencesModel, Preferences>();
        }
    }
}
