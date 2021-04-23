using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Commands.CreateCompanyContent
{
    public class CreateCompanyContentCommand : IRequest<CompanyContentDto>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<CreateCompanyContentCommand, CompanyContentDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CompanyContentDto> Handle(CreateCompanyContentCommand request, CancellationToken cancellationToken)
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

                if (success) return _mapper.Map<CompanyContent, CompanyContentDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }

    }
}
