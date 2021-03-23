using FluentValidation;

namespace Application.Opinions.Commands.CreateOpinion
{
    public class CreateOpinionCommandValidator : AbstractValidator<CreateOpinionCommand>
    {
        public CreateOpinionCommandValidator()
        {
            RuleFor(x => x.Content)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(300).WithMessage("Name maximum length is 300")
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId field is required");

            RuleFor(x => x.RateValue)
                .LessThan(6).WithMessage("RateValue can be max 5")
                .GreaterThan(0).WithMessage("RateValue must be at least 1");

        }
    }
}
