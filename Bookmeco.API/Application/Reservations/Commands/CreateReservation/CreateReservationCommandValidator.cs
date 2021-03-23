using FluentValidation;

namespace Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.ServiceCategoryId).NotNull();
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.ReservationDuration).NotNull();
            RuleFor(x => x.Prize).NotNull();
        }
    }
}
