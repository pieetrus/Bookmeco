using FluentValidation;

namespace Application.PersonsData.Commands.UpdatePersonData
{
    public class UpdatePersonDataCommandValidator : AbstractValidator<UpdatePersonDataCommand>
    {
        public UpdatePersonDataCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId field is required");

            RuleFor(x => x.FirstName)
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(x => x.LastName)
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(x => x.Email)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(80);
        }
    }
}
