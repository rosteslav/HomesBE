using AutoMapper;
using Demo.Application.Features.Properties.Commands.AddProperty;
using Demo.Application.Features.Properties.Commands.DeleteProperty;
using Demo.Application.Features.Properties.Commands.EditProperty;
using Demo.Domain.Enities;

namespace Demo.Application.Mapper
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, EditPropertyCommand>()
                .ReverseMap();

            CreateMap<Property, DeletePropertyCommand>()
                .ReverseMap();

            CreateMap<Property, AddPropertyCommand>()
                .ReverseMap();

            CreateMap<AddPropertyCommand, Property>()
                .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(x => x.NumberOfRooms, opt => opt.MapFrom(src => src.NumberOfRooms))
                .ForMember(x => x.District, opt => opt.MapFrom(src => src.District))
                .ForMember(x => x.Space, opt => opt.MapFrom(src => src.Space))
                .ForMember(x => x.Floor, opt => opt.MapFrom(src => src.Floor))
                .ForMember(x => x.TotalFloorsInBuilding, opt => opt.MapFrom(src => src.TotalFloorsInBuilding))
                .ForMember(x => x.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.BrokerId))
                .ReverseMap();

            CreateMap<DeletePropertyCommand, Property>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<EditPropertyCommand, Property>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(x => x.NumberOfRooms, opt => opt.MapFrom(src => src.NumberOfRooms))
                .ForMember(x => x.District, opt => opt.MapFrom(src => src.District))
                .ForMember(x => x.Space, opt => opt.MapFrom(src => src.Space))
                .ForMember(x => x.Floor, opt => opt.MapFrom(src => src.Floor))
                .ForMember(x => x.TotalFloorsInBuilding, opt => opt.MapFrom(src => src.TotalFloorsInBuilding))
                .ForMember(x => x.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.BrokerId))
                .ReverseMap();
        }
    }
}
