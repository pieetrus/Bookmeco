using FluentValidation;

namespace Application.CompaniesContent.Commands.CreateCompanyContent
{
    public class CreateCompanyContentCommandValidator : AbstractValidator<CreateCompanyContentCommand>
    {
        public CreateCompanyContentCommandValidator()
        {
            RuleFor(x => x.Content)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(300).WithMessage("Name maximum length is 300")
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.CompanyId).NotNull();
            RuleFor(x => x.Name);
        }
    }
}
