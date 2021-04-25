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
            CreateMap<ScheduleDay, ScheduleDayDto>()
                .ForMember(x => x.ReservationIds,
                    x => x.MapFrom(c => c.Reservations.Select(x => x.Id)));
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.UserIds,
                x => x.MapFrom(c => c.Users.Select(x => x.Id)));
            CreateMap<User, UserDto>()
                .ForMember(x => x.ServiceCategoriesIds,
                    x => x.MapFrom(c => c.ServiceCategories.Select(x => x.Id)))
                .ForMember(x => x.ReservationIds,
                    x => x.MapFrom(c => c.Reservations.Select(x => x.Id)))
                .ForMember(x => x.ScheduleIds,
                    x => x.MapFrom(c => c.Schedules.Select(x => x.Id)));
            CreateMap<User, UserLoginDto>();
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(x => x.ScheduleDayIds,
                    x => x.MapFrom(c => c.ScheduleDays.Select(x => x.Id)));
            CreateMap<ServiceCategory, ServiceCategoryDto>()
                .ForMember(x => x.UserIds,
                    x => x.MapFrom(c => c.Users.Select(x => x.Id)));
        }
    }
}
