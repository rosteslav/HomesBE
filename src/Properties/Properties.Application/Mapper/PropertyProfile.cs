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
            CreateMap<Property, PropertyModel>()
                .ReverseMap();

            CreateMap<AddPropertyCommand, Property>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.Model.BrokerId))
                .ReverseMap();
        }
    }
}
