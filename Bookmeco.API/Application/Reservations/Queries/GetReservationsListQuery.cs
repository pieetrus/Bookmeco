using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Reservations.Queries
{
    public class GetReservationsListQuery : IRequest<IEnumerable<ReservationDto>>
    {
    }

    public class GetReservationsListQueryHandler : IRequestHandler<GetReservationsListQuery, IEnumerable<ReservationDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetReservationsListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsListQuery request, CancellationToken cancellationToken)
        {
            var opinions = await _context.Reservations
                .Include(x => x.Opinions)
                .Include(x => x.PersonData)
                .Include(x => x.Schedule)
                .Include(x => x.ServiceCategory)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationDto>>(opinions);
        }
    }
}
