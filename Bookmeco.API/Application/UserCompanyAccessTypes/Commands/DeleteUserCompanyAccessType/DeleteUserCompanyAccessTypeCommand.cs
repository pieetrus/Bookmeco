using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Commands.DeleteUserCompanyAccessType
{

    public class DeleteUserCompanyAccessTypeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUserCompanyAccessTypeCommandHandler : IRequestHandler<DeleteUserCompanyAccessTypeCommand>
    {
        private readonly IDataContext _context;

        public DeleteUserCompanyAccessTypeCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserCompanyAccessTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserCompanyAccessTypes
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(UserCompanyAccessType), request.Id);
            }

            _context.UserCompanyAccessTypes.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
