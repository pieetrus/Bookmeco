using FluentValidation;

namespace Application.CompanyCategories.Commands.CreateCompanyCategory
{
    public class CreateCompanyCategoryCommandValidator : AbstractValidator<CreateCompanyCategoryCommand>
    {
        public CreateCompanyCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(30).WithMessage("Name maximum length is 30")
                .NotEmpty().WithMessage("Name field is required");
        }
    }
}
