using FluentValidation;

namespace Application.UserCompanyAccessTypes.Commands.UpdateUserCompanyAccessType
{
    public class UpdateUserCompanyAccessTypeCommandValidator : AbstractValidator<UpdateUserCompanyAccessTypeCommand>
    {
        public UpdateUserCompanyAccessTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }
}
