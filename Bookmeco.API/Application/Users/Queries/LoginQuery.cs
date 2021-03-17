using FluentValidation;
using MediatR;

namespace Application.Users.Queries
{
    public class LoginQuery : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class QueryValidator : AbstractValidator<LoginQuery>
    {
        public QueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
