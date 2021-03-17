using Application.Common.Validators;
using FluentValidation;

namespace Application.Users.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
