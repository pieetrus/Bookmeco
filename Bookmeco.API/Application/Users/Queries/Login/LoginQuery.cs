using Application.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Users.Queries.Login
{
    public class LoginQuery : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
