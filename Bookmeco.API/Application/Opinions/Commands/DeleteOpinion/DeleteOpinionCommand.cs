using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Opinions.Commands.DeleteOpinion
{
    public class DeleteOpinionCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteOpinionCommandHandler : IRequestHandler<DeleteOpinionCommand>
    {
        private readonly IDataContext _context;

        public DeleteOpinionCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOpinionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Opinions.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Opinion), request.Id);
            }

            _context.Opinions.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
