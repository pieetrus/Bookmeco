using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteServiceCategoryCommandHandler : IRequestHandler<DeleteServiceCategoryCommand>
    {
        private readonly IDataContext _context;

        public DeleteServiceCategoryCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ServiceCategories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ServiceCategory), request.Id);
            }

            _context.ServiceCategories.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
