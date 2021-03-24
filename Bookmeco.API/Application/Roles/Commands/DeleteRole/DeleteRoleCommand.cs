using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IDataContext _context;

        public DeleteRoleCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Roles.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            _context.Roles.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
