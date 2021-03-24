using FluentValidation;

namespace Application.Schedules.Commands.CreateSchedule
{
    class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(x => x.IsAvailable).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
