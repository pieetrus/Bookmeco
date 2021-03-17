﻿using Application.Companies;
using AutoMapper;
using Domain.Entities;
using System.Linq;
using Application.DTOs;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(x => x.Content,
                    x => x.MapFrom(c => c.Content.Content))
                .ForMember(x => x.UserIds,
                    x => x.MapFrom(c => c.Users.Select(u => u.Id)))
                .ForMember(x => x.Categories,
                    x => x.MapFrom(c => c.Categories.Select(ca => ca.Name)));
        }
    }
}
