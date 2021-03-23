using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Commands.CreateCompanyCategory
{
    public class CreateCompanyCategoryCommand : IRequest<int>
    {

        public string Name { get; set; }
        public int? SuperCompanyCategoryId { get; set; }

        public class Handler : IRequestHandler<CreateCompanyCategoryCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCompanyCategoryCommand request, CancellationToken cancellationToken)
            {

                var entity = new CompanyCategory { Name = request.Name };

                if (request.SuperCompanyCategoryId != null)
                {
                    var superCompanyCategory = await _context.CompanyCategories.FindAsync(request.SuperCompanyCategoryId);

                    if (superCompanyCategory == null)
                        throw new NotFoundException(nameof(CompanyCategory), request.SuperCompanyCategoryId);

                    entity.SuperCompanyCategory = superCompanyCategory;
                }

                _context.CompanyCategories.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
