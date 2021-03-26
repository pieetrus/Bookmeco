using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Commands.DeleteCompanyContent
{

    public class DeleteCompanyContentCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCompanyContentCommandHandler : IRequestHandler<DeleteCompanyContentCommand>
    {
        private readonly IDataContext _context;

        public DeleteCompanyContentCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompanyContentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CompanyContents.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CompanyContent), request.Id);
            }

            _context.CompanyContents.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
