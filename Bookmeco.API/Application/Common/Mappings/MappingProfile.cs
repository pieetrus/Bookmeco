using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(x => x.Contents,
                    x => x.MapFrom(c => c.Contents))
                .ForMember(x => x.Categories,
                    x => x.MapFrom(c => c.Categories.Select(ca => ca.Name)))
                .ForMember(x => x.UserIds,
                    x => x.MapFrom(c => c.UserCompanies.Select(x => x.User.Id)));

            CreateMap<CompanyCategory, CompanyCategoryDto>()
                .ForMember(x => x.CompanyIds,
                    x => x.MapFrom(c => c.Companies.Select(x => x.Id)));

            CreateMap<CompanyContent, CompanyContentDto>();
            CreateMap<UserCompany, UserCompanyDto>();
            CreateMap<UserCompanyAccessType, UserCompanyAccessTypeDto>();
            CreateMap<Opinion, OpinionDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.UserIds,
                x => x.MapFrom(c => c.Users.Select(x => x.Id)));
            CreateMap<User, UserDto>()
                .ForMember(x => x.ServiceCategoriesIds,
                x => x.MapFrom(c => c.ServiceCategories.Select(x => x.Id)));
            CreateMap<User, UserLoginDto>();
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ServiceCategory, ServiceCategoryDto>()
                .ForMember(x => x.UserIds,
                    x => x.MapFrom(c => c.Users.Select(x => x.Id)));
        }
    }
}
