using AutoMapper;
using BuildingMarket.Admins.Application.Features.Admins.Commands.AddNeighbourhoodsRating;
using BuildingMarket.Admins.Application.Models;
using BuildingMarket.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace BuildingMarket.Admins.Application.Mapper
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<IdentityUser, BrokerModel>();

            CreateMap<AddNeighbourhoodsRatingCommand, NeighbourhoodsRatingModel>();
        }
    }
}
