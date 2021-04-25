using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Commands.CreateSchedule
{

    public class CreateScheduleCommand : IRequest<ScheduleDto>
    {
        public int UserId { get; set; }
        public bool IsAvailable { get; set; }

        public class Handler : IRequestHandler<CreateScheduleCommand, ScheduleDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
            {
                var entity = new Schedule
                {
                    UserId = request.UserId,
                    IsAvailable = request.IsAvailable
                };

                _context.Schedules.Add(entity);


                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.UserId, cancellationToken);
                entity.User = user;

                if (success) return _mapper.Map<Schedule, ScheduleDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
