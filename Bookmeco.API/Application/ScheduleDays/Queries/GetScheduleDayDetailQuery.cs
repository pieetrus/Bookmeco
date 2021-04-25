using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Queries
{
    public class GetScheduleDayDetailQuery : IRequest<ScheduleDayDto>
    {
        public int Id { get; set; }
    }

    public class GetReservationDetailQueryHandler : IRequestHandler<GetScheduleDayDetailQuery, ScheduleDayDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetReservationDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScheduleDayDto> Handle(GetScheduleDayDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ScheduleDays
                .Include(x => x.Reservations)
                .Include(x => x.Schedule)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(ScheduleDay), request.Id);

            return _mapper.Map<ScheduleDay, ScheduleDayDto>(entity);
        }
    }
}
