using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PersonsData.Commands.UpdatePersonData
{
    public class UpdatePersonDataCommand : IRequest
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        public class Handler : IRequestHandler<UpdatePersonDataCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdatePersonDataCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PersonsData.FindAsync(request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(Opinion), request.Id);

                if (request.UserId.HasValue && !await _context.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken))
                    throw new NotFoundException(nameof(User), request.UserId);


                entity.PhoneNumber = request.PhoneNumber ?? entity.PhoneNumber;
                entity.FirstName = request.FirstName ?? entity.FirstName;
                entity.LastName = request.LastName ?? entity.LastName;
                entity.Email = request.Email ?? entity.Email;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
