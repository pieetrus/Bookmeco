using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Opinions.Commands.CreateOpinion
{
    public class CreateOpinionCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public int? RateValue { get; set; }
        public int ReservationId { get; set; }
        public int? SuperOpinionId { get; set; }

        public class Handler : IRequestHandler<CreateOpinionCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateOpinionCommand request, CancellationToken cancellationToken)
            {

                var entity = new Opinion();

                var user = await _context.Users.FindAsync(request.UserId);

                if (user == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                var reservation = await _context.Reservations.FindAsync(request.ReservationId);

                if (reservation == null)
                    throw new NotFoundException(nameof(Reservation), request.ReservationId);

                if (request.SuperOpinionId != null)
                {
                    var superOpinion = await _context.Opinions.FindAsync(request.SuperOpinionId);

                    if (superOpinion == null)
                        throw new NotFoundException(nameof(Opinion), request.SuperOpinionId);

                    entity.SuperOpinion = superOpinion;
                }

                entity.User = user;
                entity.Reservation = reservation;
                entity.Content = request.Content;
                entity.RateValue = request.RateValue;


                _context.Opinions.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
