using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Queries
{
    public class GetRoleDetailQuery : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }

    public class GetReservationDetailQueryHandler : IRequestHandler<GetRoleDetailQuery, RoleDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetReservationDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(GetRoleDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Roles
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            return _mapper.Map<Role, RoleDto>(entity);
        }
    }
}
