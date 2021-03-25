using FluentValidation;

namespace Application.UserCompanyAccessTypes.Commands.CreateUserCompanyAccessType
{
    public class CreateUserCompanyAccessTypeCommandValidator : AbstractValidator<CreateUserCompanyAccessTypeCommand>
    {
        public CreateUserCompanyAccessTypeCommandValidator()
        {
            RuleFor(x => x.AccessLevel).NotNull();
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }
}
