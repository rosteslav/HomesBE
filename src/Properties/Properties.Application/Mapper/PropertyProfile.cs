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

            CreateMap<Image, string>().ConvertUsing(img => img.ImageURL);

            CreateMap<PropertyProjectToModel, PropertyModel>()
                .ForMember(x => x.CreatedOnLocalTime, opt => opt.MapFrom(src => src.Property.CreatedOnUtcTime.ToLocalTime()))
                .ForMember(x => x.Exposure, opt => opt.MapFrom(src => src.Property.Exposure))
                .ForMember(x => x.Furnishment, opt => opt.MapFrom(src => src.Property.Furnishment))
                .ForMember(x => x.Neighbourhood, opt => opt.MapFrom(src => src.Property.Neighbourhood))
                .ForMember(x => x.BrokerId, opt => opt.MapFrom(src => src.Property.BrokerId))
                .ForMember(x => x.BuildingType, opt => opt.MapFrom(src => src.Property.BuildingType))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Property.Description))
                .ForMember(x => x.Finish, opt => opt.MapFrom(src => src.Property.Finish))
                .ForMember(x => x.Floor, opt => opt.MapFrom(src => src.Property.Floor))
                .ForMember(x => x.Garage, opt => opt.MapFrom(src => src.Property.Garage))
                .ForMember(x => x.Heating, opt => opt.MapFrom(src => src.Property.Heating))
                .ForMember(x => x.NumberOfRooms, opt => opt.MapFrom(src => src.Property.NumberOfRooms))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Property.Price))
                .ForMember(x => x.Space, opt => opt.MapFrom(src => src.Property.Space))
                .ForMember(x => x.TotalFloorsInBuilding, opt => opt.MapFrom(src => src.Property.TotalFloorsInBuilding))
                .ForMember(x => x.ContactInfo, opt => opt.MapFrom(src => new ContactInfo
                {
                    Email = src.User.Email,
                    FirstName = src.UserData.FirstName,
                    LastName = src.UserData.LastName,
                    PhoneNumber = src.UserData.PhoneNumber
                }))
                .IncludeAllDerived();

            CreateMap<PropertyProjectToModel, PropertyModelWithId>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Property.Id));

            CreateMap<Property, AddPropertyCommand>()
                .ForMember(x => x.Model, opt => opt.MapFrom(src => src))
                .ReverseMap();
        }
    }
}
