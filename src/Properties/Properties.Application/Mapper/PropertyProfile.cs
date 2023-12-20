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

            CreateMap<Property, PropertyModel>()
                .ForMember(x => x.CreatedOnLocalTime, opt => opt.MapFrom(src => src.CreatedOnUtcTime.ToLocalTime()))
                .IncludeAllDerived();

            CreateMap<Property, PropertyModelWithId>();

            CreateMap<PropertyProjectToModel, PropertyModel>()
                .IncludeMembers(src => src.Property)
                .ForMember(x => x.ContactInfo, opt => opt.MapFrom(src => new ContactInfo
                {
                    Email = src.User.Email,
                    FirstName = src.UserData.FirstName,
                    LastName = src.UserData.LastName,
                    PhoneNumber = src.UserData.PhoneNumber,
                    ImageURL = src.UserData.ImageURL
                }))
                .IncludeAllDerived();

            CreateMap<PropertyProjectToModel, PropertyModelWithId>().IncludeMembers(src => src.Property);

            CreateMap<Property, AddPropertyCommand>()
                .ForMember(x => x.Model, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<PublishedOn, PublishedOnModel>();
            CreateMap<OrderBy, OrderByModel>();

            CreateMap<PropertyDetailsWithImagesModel, GetAllPropertiesOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Property.Id))
                .ForMember(dest => dest.CreatedOnLocalTime, opt => opt.MapFrom(src => src.Property.CreatedOnUtcTime.ToLocalTime()))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src =>
                    string.Join(',', src.Property.BuildingType, src.Property.Finish, src.Property.Furnishment, src.Property.Heating, src.Property.Exposure)))
                .ForMember(dest => dest.Neighbourhood, opt => opt.MapFrom(src => src.Property.Neighbourhood))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Property.Price))
                .ForMember(dest => dest.NumberOfRooms, opt => opt.MapFrom(src => src.Property.NumberOfRooms))
                .ForMember(dest => dest.Space, opt => opt.MapFrom(src => src.Property.Space))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.OrderBy(img => img.Id).Select(img => img.ImageURL)));
        }
    }
}
