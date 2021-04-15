using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Reservations.Queries
{
    public class GetReservationDetailQuery : IRequest<ReservationDto>
    {
        public int Id { get; set; }
    }

    public class GetReservationDetailQueryHandler : IRequestHandler<GetReservationDetailQuery, ReservationDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetReservationDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Handle(GetReservationDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Reservations
                .Include(x => x.Opinions)
                .Include(x => x.User)
                .Include(x => x.ServiceCategory)
                .Include(x => x.Schedule)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Reservation), request.Id);
            }

            return _mapper.Map<Reservation, ReservationDto>(entity);
        }
    }
}
