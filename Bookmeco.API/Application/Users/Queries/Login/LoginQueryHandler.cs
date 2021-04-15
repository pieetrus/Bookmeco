using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly IDataContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IDataContext context, SignInManager<User> signInManager, IJwtGenerator jwtGenerator, IMapper mapper)
        {
            _context = context;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {

                var userDto = _mapper.Map<User, UserDto>(user);
                userDto.Roles = user.Roles.Select(r => r.Name).ToList();
                userDto.Token = _jwtGenerator.CreateToken(user);

                return userDto;
            }

            throw new UnauthorizedException(request.UserName);
        }
    }
}
