using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IDataContext _context;

        public DeleteCompanyCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Companies.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            _context.Companies.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
