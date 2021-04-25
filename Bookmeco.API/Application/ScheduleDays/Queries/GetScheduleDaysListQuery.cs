using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Queries
{
    public class GetScheduleDaysListQuery : IRequest<IEnumerable<ScheduleDayDto>>
    {
        public int? ScheduleId { get; set; }
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
            var queryable = _context.ScheduleDays
                .Include(x => x.Reservations)
                .Include(x => x.Schedule)
                .AsQueryable();

            if (request.ScheduleId != null)
                queryable = queryable.Where(x => x.ScheduleId == request.ScheduleId);

            var scheduleDays = await queryable.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ScheduleDay>, IEnumerable<ScheduleDayDto>>(scheduleDays);
        }
    }
}
