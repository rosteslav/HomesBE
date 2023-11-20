using AutoMapper;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;

namespace BuildingMarket.Images.Application.Mapper
{
    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            CreateMap<Image, ImagesResult>();
        }
    }
}
