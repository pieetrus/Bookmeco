using FluentValidation;

namespace Application.Schedules.Commands.CreateSchedule
{
    public class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(x => x.IsAvailable).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
