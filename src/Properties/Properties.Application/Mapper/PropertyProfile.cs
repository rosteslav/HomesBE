using AutoMapper;
using BuildingMarket.Common.Models;
using BuildingMarket.Properties.Application.Features.Properties.Commands.AddProperty;
using BuildingMarket.Properties.Application.Features.Properties.Commands.ReportProperty;
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

            CreateMap<Property, GetAllPropertiesOutputModel>()
                .ForMember(x => x.CreatedOnLocalTime, opt => opt.MapFrom(src => src.CreatedOnUtcTime.ToLocalTime()))
                .ForMember(x => x.Details, opt => opt.MapFrom(src => string.Join(',', src.BuildingType, src.Finish, src.Furnishment, src.Heating, src.Exposure)));

            CreateMap<Property, PropertyRedisModel>();

            CreateMap<AddPropertyInputModel, PropertyRedisModel>();

            CreateMap<PublishedOn, PublishedOnModel>();
            CreateMap<OrderBy, OrderByModel>();

            CreateMap<ReportPropertyCommand, ReportRedisModel>()
                .BeforeMap((_, d) => d.TimeStamp = DateTime.UtcNow)
                .ForMember(x => x.Reason,
                    opt => opt.MapFrom(src => src.ReportModel.Reason));
        }
    }
}
