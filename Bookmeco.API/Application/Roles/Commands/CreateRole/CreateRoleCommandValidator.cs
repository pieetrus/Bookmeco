using FluentValidation;

namespace Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.AccessLevel).NotNull();
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }
}
