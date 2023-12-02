using AutoMapper;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty;
using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;

namespace BuildingMarket.Properties.Application.Mapper
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<AddPropertyInputModel, Property>();

            CreateMap<Property, PropertyModel>()
                .ForMember(x => x.CreatedOnLocalTime, opt => opt.MapFrom(src => src.CreatedOnUtcTime.ToLocalTime()))
                .ReverseMap()
                .ForPath(src => src.CreatedOnUtcTime, opt => opt.MapFrom(x => x.CreatedOnLocalTime.ToUniversalTime()));

            CreateMap<Property, AddPropertyCommand>()
                .ForMember(x => x.Model, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<PublishedOn, PublishedOnModel>();
        }
    }
}
