using AutoMapper;
using BuildingMarket.Images.Application.Models;
using BuildingMarket.Images.Domain.Entities;

namespace BuildingMarket.Images.Application.Mapper
{
    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            CreateMap<Image, ImagesResult>()
                .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageURL,
                            opt => opt.MapFrom(src => src.ImageURL));
        }
    }
}
