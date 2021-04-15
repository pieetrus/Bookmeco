using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommand : IRequest
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; } //todo: add logic for schedule
        public int? ServiceCategoryId { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public int? ReservationDuration { get; set; }
        public float? Prize { get; set; }

        public class Handler : IRequestHandler<UpdateReservationCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
            {

                var entity = await _context.Reservations.FindAsync(request.Id, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(entity), request.Id);

                if (request.UserId != null)
                {
                    var personData = await _context.PersonsData.FirstOrDefaultAsync(x => x.User.Id == request.UserId, cancellationToken);

                    if (personData == null)
                        throw new NotFoundException(nameof(personData), request.UserId);

                    entity.PersonDataId = personData.Id;
                }

                if (request.ScheduleId != null && !await _context.Schedules.AnyAsync(x => x.Id == request.ScheduleId, cancellationToken))
                    throw new NotFoundException(nameof(Schedule), request.ScheduleId);

                if (request.ServiceCategoryId != null && !await _context.ServiceCategories.AnyAsync(x => x.Id == request.ServiceCategoryId, cancellationToken))
                    throw new NotFoundException(nameof(ServiceCategory), request.ServiceCategoryId);

                entity.ScheduleId = request.ScheduleId ?? entity.ScheduleId;
                entity.ServiceCategoryId = request.ServiceCategoryId ?? entity.ServiceCategoryId;
                entity.Date = request.Date ?? entity.Date;
                entity.ReservationDuration = request.ReservationDuration ?? entity.ReservationDuration;
                entity.Prize = request.Prize ?? entity.Prize;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
