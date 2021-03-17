using FluentValidation;

namespace Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(30).WithMessage("Name maximum length is 30")
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.Address)
                .MinimumLength(3).WithMessage("Address maximum length is 3")
                .MaximumLength(100).WithMessage("Address maximum length is 100")
                .NotEmpty().WithMessage("Address field is required");

            RuleFor(x => x.Latitude)
                .NotEmpty().WithMessage("Latitude field is required");

            RuleFor(x => x.Longitude)
                .NotEmpty().WithMessage("Longitude field is required");
        }
    }
}
