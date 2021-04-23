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

namespace Application.Opinions.Commands.CreateOpinion
{
    public class CreateOpinionCommand : IRequest<OpinionDto>
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public int? RateValue { get; set; }
        public int ReservationId { get; set; }
        public int? SuperOpinionId { get; set; }

        public class Handler : IRequestHandler<CreateOpinionCommand, OpinionDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OpinionDto> Handle(CreateOpinionCommand request, CancellationToken cancellationToken)
            {
                var entity = new Opinion();

                var user = await _context.Users
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (user == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(x => x.Id == request.ReservationId, cancellationToken);

                if (reservation == null)
                    throw new NotFoundException(nameof(Reservation), request.ReservationId);

                if (request.SuperOpinionId != null)
                {
                    var superOpinion = await _context.Opinions
                        .FirstOrDefaultAsync(x => x.Id == request.SuperOpinionId);

                    if (superOpinion == null)
                        throw new NotFoundException(nameof(Opinion), request.SuperOpinionId);

                    entity.SuperOpinion = superOpinion;
                }

                entity.User = user;
                entity.Reservation = reservation;
                entity.Content = request.Content;
                entity.RateValue = request.RateValue;
                entity.Date = DateTime.Now;

                _context.Opinions.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map<Opinion, OpinionDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }

    }
}
