using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PersonsData.Commands.CreatePersonData
{
    public class CreatePersonDataCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class Handler : IRequestHandler<CreatePersonDataCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreatePersonDataCommand request, CancellationToken cancellationToken)
            {
                if (!await _context.Users.AnyAsync(x => x.Id == request.UserId))
                    throw new NotFoundException(nameof(User), request.UserId);

                var entity = new PersonData
                {
                    UserId = request.UserId,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber
                };

                _context.PersonsData.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
