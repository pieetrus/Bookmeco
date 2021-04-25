using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<RoleDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }

        public class Handler : IRequestHandler<UpdateRoleCommand, RoleDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Roles
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Role), request.Id);
                }

                entity.Name = request.Name;
                entity.AccessLevel = request.AccessLevel;
                entity.NormalizedName = request.Name.ToUpper();

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Role, RoleDto>(entity);

            }
        }
    }
}
