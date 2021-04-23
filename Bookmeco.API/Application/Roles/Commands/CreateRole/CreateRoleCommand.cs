using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<RoleDto>
    {
        public string Name { get; set; }
        public int AccessLevel { get; set; }

        public class Handler : IRequestHandler<CreateRoleCommand, RoleDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = new Role
                {
                    Name = request.Name,
                    AccessLevel = request.AccessLevel,
                    NormalizedName = request.Name.ToUpper()
                };

                _context.Roles.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map<Role, RoleDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
