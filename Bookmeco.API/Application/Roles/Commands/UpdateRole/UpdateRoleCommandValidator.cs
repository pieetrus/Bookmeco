using FluentValidation;

namespace Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }
}
