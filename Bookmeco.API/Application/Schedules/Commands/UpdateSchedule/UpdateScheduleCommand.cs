using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Commands.UpdateSchedule
{
    public class UpdateScheduleCommand : IRequest<ScheduleDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsAvailable { get; set; }

        public class Handler : IRequestHandler<UpdateScheduleCommand, ScheduleDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Schedules
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Schedule), request.Id);
                }

                if (request.UserId != entity.UserId &&
                    !await _context.Users.AnyAsync(x => x.Id == request.UserId))
                {
                    throw new NotFoundException(nameof(User), request.UserId);
                }

                entity.UserId = request.UserId;
                entity.IsAvailable = request.IsAvailable;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Schedule, ScheduleDto>(entity);
            }
        }
    }
}
