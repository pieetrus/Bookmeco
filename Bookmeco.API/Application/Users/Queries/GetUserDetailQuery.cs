using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUserDetailQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }

    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDto>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .Include(x => x.Roles)
                .Include(x => x.UserCompanies)
                .Include(x => x.ServiceCategories)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(entity), request.Id);

            return _mapper.Map<User, UserDto>(entity);
        }
    }
}
