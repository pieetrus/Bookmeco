using FluentValidation;

namespace Application.ScheduleDays.Commands.CreateScheduleDay
{
    public class CreateScheduleDayCommandValidator : AbstractValidator<CreateScheduleDayCommand>
    {
        public CreateScheduleDayCommandValidator()
        {
            RuleFor(x => x.ScheduleId).NotNull();
            RuleFor(x => x.BeginTime).NotNull();
            RuleFor(x => x.EndTime).NotNull();
            RuleFor(x => x.DayOfWeek).NotNull();
            RuleFor(x => x.IsRegular).NotNull();
        }
    }
}
