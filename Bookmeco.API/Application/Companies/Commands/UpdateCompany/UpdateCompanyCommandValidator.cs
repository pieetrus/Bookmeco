using FluentValidation;

namespace Application.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(30).WithMessage("Name maximum length is 30");

            RuleFor(x => x.Address)
                .MinimumLength(3).WithMessage("Address minimum length is 3")
                .MaximumLength(100).WithMessage("Address maximum length is 100");
        }
    }
}
