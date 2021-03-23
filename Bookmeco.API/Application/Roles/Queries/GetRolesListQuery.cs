using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Queries
{
    public class GetRolesListQuery : IRequest<IEnumerable<RoleDto>>
    {
    }

    public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, IEnumerable<RoleDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetRolesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var opinions = await _context.Roles
                .Include(x => x.Users)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDto>>(opinions);
        }
    }
}
