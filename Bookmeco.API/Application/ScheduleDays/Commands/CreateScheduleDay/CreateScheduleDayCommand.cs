using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Commands.CreateScheduleDay
{
    public class CreateScheduleDayCommand : IRequest<ScheduleDayDto>
    {
        [JsonIgnore]
        public int ScheduleId { get; set; }
        public int BeginTime { get; set; }
        public int EndTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRegular { get; set; }
        public int? MaxClients { get; set; }

        public class Handler : IRequestHandler<CreateScheduleDayCommand, ScheduleDayDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ScheduleDayDto> Handle(CreateScheduleDayCommand request, CancellationToken cancellationToken)
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

                if (success) return _mapper.Map<ScheduleDay, ScheduleDayDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
