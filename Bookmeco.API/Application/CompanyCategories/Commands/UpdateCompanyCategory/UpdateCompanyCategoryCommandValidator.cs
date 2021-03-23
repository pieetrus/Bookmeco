using FluentValidation;

namespace Application.CompanyCategories.Commands.UpdateCompanyCategory
{
    public class UpdateCompanyCategoryCommandValidator : AbstractValidator<UpdateCompanyCategoryCommand>
    {
        public UpdateCompanyCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(30).WithMessage("Name maximum length is 30");
        }
    }
}
