using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUsersListQuery : IRequest<IEnumerable<UserDto>>
    {
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
            var users = await _context.Users
                .Include(x => x.Roles)
                .Include(x => x.UserCompanies)
                .Include(x => x.ServiceCategories)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }
    }
}
