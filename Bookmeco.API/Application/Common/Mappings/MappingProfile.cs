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
                .ForMember(x => x.Content,
                    x => x.MapFrom(c => c.Content.Content))
                .ForMember(x => x.Categories,
                    x => x.MapFrom(c => c.Categories.Select(ca => ca.Name)));

            CreateMap<CompanyCategory, CompanyCategoryDto>();
            CreateMap<UserCompany, UserCompanyDto>();
            CreateMap<Opinion, OpinionDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<PersonData, PersonDataDto>();
        }
    }
}
