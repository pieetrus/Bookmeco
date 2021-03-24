using FluentValidation;

namespace Application.PersonsData.Commands.CreatePersonData
{
    public class CreatePersonDataCommandValidator : AbstractValidator<CreatePersonDataCommand>
    {
        public CreatePersonDataCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId field is required");

            RuleFor(x => x.FirstName)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(x => x.LastName)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(x => x.Email)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(80);

        }
    }
}
