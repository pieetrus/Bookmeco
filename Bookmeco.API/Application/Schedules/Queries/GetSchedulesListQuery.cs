using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Queries
{
    public class GetSchedulesListQuery : IRequest<IEnumerable<ScheduleDto>>
    {
    }

    public class GetSchedulesListQueryHandler : IRequestHandler<GetSchedulesListQuery, IEnumerable<ScheduleDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetSchedulesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> Handle(GetSchedulesListQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _context.Schedules
                .Include(x => x.ScheduleDays)
                .Include(x => x.User)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleDto>>(schedules);
        }
    }
}
