using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Commands.UpdateScheduleDay
{
    public class UpdateScheduleDayCommand : IRequest
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsRegular { get; set; }
        public int? MaxClients { get; set; }

        public class Handler : IRequestHandler<UpdateScheduleDayCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateScheduleDayCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ScheduleDays.FindAsync(request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Schedule), request.Id);

                if (request.ScheduleId != null && !await _context.Schedules.AnyAsync(x => x.Id == request.ScheduleId))
                    throw new NotFoundException(nameof(ScheduleDay), request.ScheduleId);

                entity.ScheduleId = request.ScheduleId ?? entity.ScheduleId;
                entity.BeginTime = request.BeginTime ?? entity.BeginTime;
                entity.EndTime = request.EndTime ?? entity.EndTime;
                entity.DayOfWeek = request.DayOfWeek ?? entity.DayOfWeek;
                entity.Date = request.Date ?? entity.Date;
                entity.IsRegular = request.IsRegular ?? entity.IsRegular;
                entity.MaxClients = request.MaxClients ?? entity.MaxClients;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
