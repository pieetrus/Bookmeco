using FluentValidation;

namespace Application.ScheduleDays.Commands.UpdateScheduleDay
{
    public class UpdateScheduleDayCommandValidator : AbstractValidator<UpdateScheduleDayCommand>
    {
        public UpdateScheduleDayCommandValidator()
        {
        }
    }
}
