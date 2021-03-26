using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Commands.CreateScheduleDay
{
    public class CreateScheduleDayCommand : IRequest<int>
    {
        public int ScheduleId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }

        public class Handler : IRequestHandler<CreateScheduleDayCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateScheduleDayCommand request, CancellationToken cancellationToken)
            {
                if (!await _context.Schedules.AnyAsync(x => x.Id == request.ScheduleId))
                    throw new NotFoundException(nameof(ScheduleDay), request.ScheduleId);

                var entity = new ScheduleDay
                {
                    ScheduleId = request.ScheduleId,
                    EndTime = request.EndTime,
                    DayOfWeek = request.DayOfWeek,
                    Date = request.Date,
                    IsRegular = request.IsRegular,
                    MaxClients = request.MaxClients,
                    BeginTime = request.BeginTime
                };

                _context.ScheduleDays.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
