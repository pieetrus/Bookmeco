using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommand : IRequest<ReservationDto>
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; } //todo: add logic for schedule
        public int ServiceCategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int? ReservationDuration { get; set; }
        public float? Prize { get; set; }

        public class Handler : IRequestHandler<UpdateReservationCommand, ReservationDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ReservationDto> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
            {
                ServiceCategory serviceCategory = null;

                var entity = await _context.Reservations
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(entity), request.Id);

                if (request.UserId != entity.UserId)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                    if (user == null)
                        throw new NotFoundException(nameof(User), request.UserId);

                    entity.UserId = user.Id;
                }

                if (request.ScheduleId != entity.ScheduleId &&
                    !await _context.Schedules.AnyAsync(x => x.Id == request.ScheduleId, cancellationToken))

                    throw new NotFoundException(nameof(Schedule), request.ScheduleId);

                if (request.ServiceCategoryId != entity.ServiceCategoryId)
                {
                    serviceCategory =
                        await _context.ServiceCategories
                        .FirstOrDefaultAsync(x => x.Id == request.ServiceCategoryId, cancellationToken);

                    if (serviceCategory == null)
                    {
                        throw new NotFoundException(nameof(ServiceCategory), request.ServiceCategoryId);
                    }
                }
                else
                {
                    serviceCategory =
                        await _context.ServiceCategories
                        .FirstOrDefaultAsync(x => x.Id == request.ServiceCategoryId, cancellationToken);
                }

                entity.ScheduleId = request.ScheduleId;
                entity.Date = request.Date;
                entity.ServiceCategoryId = request.ServiceCategoryId;
                entity.ReservationDuration = request.ReservationDuration ?? serviceCategory.ServiceDuration;
                entity.Prize = request.Prize ?? serviceCategory.Prize;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Reservation, ReservationDto>(entity);
            }
        }
    }
}
