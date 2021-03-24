using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Queries
{
    public class GetScheduleDaysListQuery : IRequest<IEnumerable<ScheduleDayDto>>
    {
    }

    public class GetScheduleDaysListQueryHandler : IRequestHandler<GetScheduleDaysListQuery, IEnumerable<ScheduleDayDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetScheduleDaysListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDayDto>> Handle(GetScheduleDaysListQuery request, CancellationToken cancellationToken)
        {
            var scheduleDays = await _context.ScheduleDays
                .Include(x => x.Schedule)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ScheduleDay>, IEnumerable<ScheduleDayDto>>(scheduleDays);
        }
    }
}
