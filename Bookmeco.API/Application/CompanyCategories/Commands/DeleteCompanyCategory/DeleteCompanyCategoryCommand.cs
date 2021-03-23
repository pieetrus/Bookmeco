using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Commands.DeleteCompanyCategory
{
    public class DeleteCompanyCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCompanyCategoryCommandHandler : IRequestHandler<DeleteCompanyCategoryCommand>
    {
        private readonly IDataContext _context;

        public DeleteCompanyCategoryCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompanyCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CompanyCategories.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CompanyCategory), request.Id);
            }

            _context.CompanyCategories.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
