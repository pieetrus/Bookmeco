using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Commands.UpdateCompanyContent
{

    public class UpdateCompanyContentCommand : IRequest<CompanyContentDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<UpdateCompanyContentCommand, CompanyContentDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CompanyContentDto> Handle(UpdateCompanyContentCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.CompanyContents
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(CompanyContent), request.Id);

                if (!await _context.Companies.AnyAsync(x => x.Id == request.CompanyId))
                    throw new NotFoundException(nameof(Company), request.CompanyId);

                entity.Content = request.Content;
                entity.Name = request.Name;
                entity.CompanyId = request.CompanyId;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<CompanyContent, CompanyContentDto>(entity);
            }
        }
    }
}
