using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanies.Commands.DeleteUserCompany
{

    public class DeleteUserCompanyCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUserCompanyCommandHandler : IRequestHandler<DeleteUserCompanyCommand>
    {
        private readonly IDataContext _context;

        public DeleteUserCompanyCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserCompanies.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(UserCompany), request.Id);
            }

            _context.UserCompanies.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
