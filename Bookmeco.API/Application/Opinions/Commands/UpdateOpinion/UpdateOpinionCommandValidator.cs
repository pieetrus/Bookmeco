using FluentValidation;

namespace Application.Opinions.Commands.UpdateOpinion
{
    public class UpdateOpinionCommandValidator : AbstractValidator<UpdateOpinionCommand>
    {
        public UpdateOpinionCommandValidator()
        {
            RuleFor(x => x.Content)
                .MinimumLength(3).WithMessage("Name minimum length is 3")
                .MaximumLength(300).WithMessage("Name maximum length is 300")
                .NotEmpty().WithMessage("Name field is required");

            RuleFor(x => x.RateValue)
                .LessThan(6).WithMessage("RateValue can be max 5")
                .GreaterThan(0).WithMessage("RateValue must be at least 1");

        }
    }

}
