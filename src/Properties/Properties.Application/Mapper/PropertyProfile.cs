using BuildingMarket.Properties.Application.Models;
using BuildingMarket.Properties.Domain.Entities;
using AutoMapper;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty;

namespace BuildingMarket.Properties.Application.Mapper
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyModel>()
                .ReverseMap();

            CreateMap<PropertyModel, Property>()
               .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Type))
               .ForMember(x => x.NumberOfRooms, opt => opt.MapFrom(src => src.NumberOfRooms))
               .ForMember(x => x.District, opt => opt.MapFrom(src => src.District))
               .ForMember(x => x.Space, opt => opt.MapFrom(src => src.Space))
               .ForMember(x => x.Floor, opt => opt.MapFrom(src => src.Floor))
               .ForMember(x => x.TotalFloorsInBuilding, opt => opt.MapFrom(src => src.TotalFloorsInBuilding))
               .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.BrokerId))
               .ReverseMap();

            CreateMap<AddPropertyCommand, Property>()
               .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Model.Type))
               .ForMember(x => x.NumberOfRooms, opt => opt.MapFrom(src => src.Model.NumberOfRooms))
               .ForMember(x => x.District, opt => opt.MapFrom(src => src.Model.District))
               .ForMember(x => x.Space, opt => opt.MapFrom(src => src.Model.Space))
               .ForMember(x => x.Floor, opt => opt.MapFrom(src => src.Model.Floor))
               .ForMember(x => x.TotalFloorsInBuilding, opt => opt.MapFrom(src => src.Model.TotalFloorsInBuilding))
               .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.Model.BrokerId))
               .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.SellerId))
               .ReverseMap();
        }
    }
}
