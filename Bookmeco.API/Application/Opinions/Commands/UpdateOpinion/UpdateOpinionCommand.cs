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

namespace Application.Opinions.Commands.UpdateOpinion
{
    public class UpdateOpinionCommand : IRequest<OpinionDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int? RateValue { get; set; }
        public int ReservationId { get; set; }
        public int? SuperOpinionId { get; set; }

        public class Handler : IRequestHandler<UpdateOpinionCommand, OpinionDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OpinionDto> Handle(UpdateOpinionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Opinions
                    .Include(x => x.User).ThenInclude(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Opinion), request.Id);
                }

                entity.Reservation = null;
                entity.User = null;
                entity.SuperOpinion = null;

                if (request.ReservationId != entity.ReservationId)
                {
                    var reservation = await _context.Reservations.FindAsync(request.ReservationId);

                    if (reservation == null)
                        throw new NotFoundException(nameof(Reservation), request.ReservationId);

                    entity.Reservation = reservation;
                }

                if (request.UserId != entity.UserId)
                {
                    var user = await _context.Users
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                    if (user == null)
                        throw new NotFoundException(nameof(User), request.UserId);

                    entity.User = user;
                }

                if (request.SuperOpinionId != entity.SuperOpinionId &&
                    request.SuperOpinionId != null)
                {
                    var opinion = await _context.Opinions.FindAsync(request.UserId);

                    if (opinion == null)
                        throw new NotFoundException(nameof(Opinion), request.UserId);

                    entity.SuperOpinion = opinion;
                }

                entity.Content = request.Content;
                entity.RateValue = request.RateValue;
                entity.Date = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Opinion, OpinionDto>(entity);
            }
        }
    }
}
