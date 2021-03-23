using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Opinions.Commands.UpdateOpinion
{
    public class UpdateOpinionCommand : IRequest
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public int? RateValue { get; set; }
        public int? ReservationId { get; set; }
        public int? SuperOpinionId { get; set; }


        public class Handler : IRequestHandler<UpdateOpinionCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateOpinionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Opinions.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Opinion), request.Id);
                }

                entity.Reservation = null;
                entity.User = null;
                entity.SuperOpinion = null;

                if (request.ReservationId != null)
                {
                    var reservation = await _context.Reservations.FindAsync(request.ReservationId);

                    if (reservation == null)
                        throw new NotFoundException(nameof(Reservation), request.ReservationId);

                    entity.Reservation = reservation;
                }

                if (request.UserId != null)
                {
                    var user = await _context.Users.FindAsync(request.UserId);

                    if (user == null)
                        throw new NotFoundException(nameof(User), request.UserId);

                    entity.User = user;
                }

                if (request.SuperOpinionId != null)
                {
                    var opinion = await _context.Opinions.FindAsync(request.UserId);

                    if (opinion == null)
                        throw new NotFoundException(nameof(Opinion), request.UserId);

                    entity.SuperOpinion = opinion;
                }

                entity.Content = request.Content ?? entity.Content;
                entity.RateValue = request.RateValue ?? entity.RateValue; //todo: what if we want to remove rate value and set to null 

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
