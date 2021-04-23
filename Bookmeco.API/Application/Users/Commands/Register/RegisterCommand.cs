using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Register
{
    public class RegisterCommand : IRequest<UserLoginDto>
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public class Handler : IRequestHandler<RegisterCommand, UserLoginDto>
        {
            private readonly IDataContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _mapper = mapper;
            }

            public async Task<UserLoginDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if (await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
                    throw new BadRequestException("Email already exist");

                if (await _context.Users.AnyAsync(x => x.UserName == request.Username, cancellationToken))
                    throw new BadRequestException("Username already exist");

                var user = new User
                {
                    Email = request.Email,
                    UserName = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                //await _userManager.AddToRoleAsync(user, "USER");

                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<User, UserLoginDto>(user);
                    userDto.Token = _jwtGenerator.CreateToken(user);

                    return userDto;
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
