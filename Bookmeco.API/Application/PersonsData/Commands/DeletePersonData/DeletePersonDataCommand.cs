using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PersonsData.Commands.DeletePersonData
{
    public class DeletePersonDataCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteOpinionCommandHandler : IRequestHandler<DeletePersonDataCommand>
    {
        private readonly IDataContext _context;

        public DeleteOpinionCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePersonDataCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PersonsData.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PersonData), request.Id);
            }

            _context.PersonsData.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
