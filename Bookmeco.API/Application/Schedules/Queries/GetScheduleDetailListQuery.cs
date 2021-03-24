using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Queries
{
    public class GetScheduleDetailListQuery : IRequest<ScheduleDto>
    {
        public int Id { get; set; }
    }

    public class GetScheduleDetailListQueryHandler : IRequestHandler<GetScheduleDetailListQuery, ScheduleDto>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetScheduleDetailListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> Handle(GetScheduleDetailListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Schedules
                .Include(x => x.Reservations)
                .Include(x => x.ScheduleDays)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<Schedule, ScheduleDto>(entity);
        }
    }
}
