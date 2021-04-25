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

namespace Application.ScheduleDays.Commands.UpdateScheduleDay
{
    public class UpdateScheduleDayCommand : IRequest<ScheduleDayDto>
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int BeginTime { get; set; }
        public int EndTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }

        public class Handler : IRequestHandler<UpdateScheduleDayCommand, ScheduleDayDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ScheduleDayDto> Handle(UpdateScheduleDayCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ScheduleDays
                    .FindAsync(request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Schedule), request.Id);

                if (request.ScheduleId != entity.ScheduleId
                    && !await _context.Schedules.AnyAsync(x => x.Id == request.ScheduleId))
                    throw new NotFoundException(nameof(ScheduleDay), request.ScheduleId);

                entity.ScheduleId = request.ScheduleId;
                entity.BeginTime = request.BeginTime;
                entity.EndTime = request.EndTime;
                entity.DayOfWeek = request.DayOfWeek;
                entity.Date = request.Date;
                entity.IsRegular = request.IsRegular;
                entity.MaxClients = request.MaxClients;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ScheduleDay, ScheduleDayDto>(entity);
            }
        }
    }
}
