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

            CreateMap<CompanyCategory, CompanyCategoryDto>();
            CreateMap<CompanyContent, CompanyContentDto>();
            CreateMap<UserCompany, UserCompanyDto>();
            CreateMap<Opinion, OpinionDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>();
            CreateMap<Schedule, ScheduleDto>();
        }
    }
}
