using Application.Common.Exceptions;
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

namespace Application.Users.Commands.AssignRoles
{
    public class AssignRolesCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }

        public class Handler : IRequestHandler<AssignRolesCommand, UserDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(AssignRolesCommand request, CancellationToken cancellationToken)
            {
                List<Role> rolesDb = null;

                var user = await _context.Users
                    .Include(x => x.Roles)
                    .Include(x => x.ServiceCategories)
                    .Include(x => x.Reservations)
                    .Include(x => x.Schedules)
                    .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (request.RoleIds != null && request.RoleIds.Any())
                {
                    rolesDb = await _context.Roles
                        .Where(x => request.RoleIds.Contains(x.Id))
                        .ToListAsync(cancellationToken);

                    if (rolesDb.Count != request.RoleIds.Count)
                    {
                        throw new NotFoundException(nameof(Role), "Some of objects on list was not found");
                    }
                }

                user.Roles = rolesDb;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<User, UserDto>(user);
            }
        }
    }
}
