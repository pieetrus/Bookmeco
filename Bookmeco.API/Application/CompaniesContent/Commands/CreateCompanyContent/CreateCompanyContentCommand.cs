using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Commands.CreateCompanyContent
{
    public class CreateCompanyContentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<CreateCompanyContentCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCompanyContentCommand request, CancellationToken cancellationToken)
            {
                if (!await _context.Companies.AnyAsync(x => x.Id == request.CompanyId))
                    throw new NotFoundException(nameof(Company), request.CompanyId);

                var entity = new CompanyContent
                {
                    Name = request.Name,
                    CompanyId = request.CompanyId,
                    Content = request.Content
                };

                _context.CompanyContents.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
