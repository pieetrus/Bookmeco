using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Commands.UpdateCompanyCategory
{
    public class UpdateCompanyCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SuperCompanyCategoryId { get; set; }


        public class Handler : IRequestHandler<UpdateCompanyCategoryCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateCompanyCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.CompanyCategories.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Company), request.Id);
                }

                if (request.SuperCompanyCategoryId != null)
                {
                    var superCategory = await _context.CompanyCategories.FindAsync(request.SuperCompanyCategoryId, cancellationToken);
                    if (superCategory == null)
                        throw new NotFoundException(nameof(CompanyCategory), request.Id);
                    entity.SuperCompanyCategory = superCategory;
                }

                entity.Name = request.Name ?? entity.Name;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
