using FluentValidation;

namespace Application.CompaniesContent.Commands.UpdateCompanyContent
{
    public class UpdateCompanyContentCommandValidator : AbstractValidator<UpdateCompanyContentCommand>
    {
        public UpdateCompanyContentCommandValidator()
        {
            RuleFor(x => x.Content)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(300).WithMessage("Name maximum length is 300");

            RuleFor(x => x.Name);
        }
    }

}
