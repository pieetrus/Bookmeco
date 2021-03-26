using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Commands.UpdateCompanyContent
{

    public class UpdateCompanyContentCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int? CompanyId { get; set; }

        public class Handler : IRequestHandler<UpdateCompanyContentCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCompanyContentCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.CompanyContents.FindAsync(request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(CompanyContent), request.Id);

                if (request.CompanyId != null && !await _context.Companies.AnyAsync(x => x.Id == request.CompanyId))
                    throw new NotFoundException(nameof(Company), request.CompanyId);


                entity.Content = request.Content ?? entity.Content;
                entity.Name = request.Name ?? entity.Name;
                entity.CompanyId = request.CompanyId ?? entity.CompanyId;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
