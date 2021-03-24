using FluentValidation;

namespace Application.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommandValidator : AbstractValidator<CreateServiceCategoryCommand>
    {
        public CreateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Prize).NotNull();
            RuleFor(x => x.ServiceDuration)
                .NotNull()
                .GreaterThan(0)
                .LessThan(2880); // less than 2 days idk why xD
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }
}
