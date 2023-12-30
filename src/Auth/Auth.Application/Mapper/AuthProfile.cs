﻿using AutoMapper;
using BuildingMarket.Auth.Application.Models.Security;
using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Common.Models;

namespace BuildingMarket.Auth.Application.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<PreferencesModel, Preferences>();

            CreateMap<RegisterModel, BuyerPreferencesRedisModel>();
            CreateMap<RegisterModel, PreferencesModel>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}
