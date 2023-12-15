using AutoMapper;
using BuildingMarket.Admins.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Mapper
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<IdentityUser, BrokerModel>();
        }
    }
}
