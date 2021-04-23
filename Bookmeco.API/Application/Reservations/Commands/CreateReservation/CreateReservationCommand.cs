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

namespace Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<ReservationDto>
    {
        public int ScheduleId { get; set; } //todo: add logic for schedule
        public int ServiceCategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int? ReservationDuration { get; set; }
        public float? Prize { get; set; }


        public class Handler : IRequestHandler<CreateReservationCommand, ReservationDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ReservationDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
            {
                var entity = new Reservation();

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (user == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                var schedule = await _context.Schedules.FindAsync(request.ScheduleId);

                if (schedule == null)
                    throw new NotFoundException(nameof(Schedule), request.ScheduleId);

                var serviceCategory = await _context.ServiceCategories.FindAsync(request.ServiceCategoryId);

                if (serviceCategory == null)
                    throw new NotFoundException(nameof(ServiceCategory), request.ServiceCategoryId);

                entity.User = user;
                entity.ServiceCategory = serviceCategory;
                entity.Schedule = schedule;
                entity.ReservationDuration = request.ReservationDuration ?? serviceCategory.ServiceDuration;
                entity.Prize = request.Prize ?? serviceCategory.Prize;
                entity.Date = request.Date;
                 
                _context.Reservations.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map<Reservation, ReservationDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
