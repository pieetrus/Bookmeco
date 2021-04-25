using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, UserDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (user == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                user.UserName = request.Username ?? user.UserName;
                user.FirstName = request.FirstName ?? user.FirstName;
                user.LastName = request.LastName ?? user.LastName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.Email = request.Email ?? user.Email;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<User, UserDto>(user);
            }
        }
    }
}
