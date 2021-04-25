using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUsersListQuery : IRequest<IEnumerable<UserDto>>
    {
        public GetUsersListQuery(int? companyId)
        {
            CompanyId = companyId;
        }

        public int? CompanyId { get; set; }
    }

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, IEnumerable<UserDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Users
                .Include(x => x.Reservations)
                .Include(x => x.Schedules)
                .Include(x => x.Roles)
                .Include(x => x.UserCompanies)
                .Include(x => x.ServiceCategories)
                .AsQueryable();

            if (request.CompanyId != null)
            {
                queryable = queryable.Where(x => x.UserCompanies.Any(x => x.CompanyId == request.CompanyId));
            }

            var users = await queryable.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }
    }
}
