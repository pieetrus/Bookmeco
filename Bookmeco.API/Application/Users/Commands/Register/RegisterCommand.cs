using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Users.Commands.Register
{
    public class RegisterCommand : IRequest<UserDto>
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<RegisterCommand, UserDto>
        {
            private readonly IDataContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(IDataContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if (await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
                    throw new BadRequestException("Email already exist");

                if (await _context.Users.AnyAsync(x => x.UserName == request.Username, cancellationToken))
                    throw new BadRequestException("Username already exist");

                var user = new User
                {
                    Email = request.Email,
                    UserName = request.Username,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                //await _userManager.AddToRoleAsync(user, "USER");

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Username = user.UserName,
                        Token = _jwtGenerator.CreateToken(user),
                    };
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
