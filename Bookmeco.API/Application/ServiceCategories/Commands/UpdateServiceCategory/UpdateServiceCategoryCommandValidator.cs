using FluentValidation;

namespace Application.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandValidator : AbstractValidator<UpdateServiceCategoryCommand>
    {
        public UpdateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.ServiceDuration)
                .GreaterThan(0)
                .LessThan(2880); // less than 2 days idk why xD
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }
}
