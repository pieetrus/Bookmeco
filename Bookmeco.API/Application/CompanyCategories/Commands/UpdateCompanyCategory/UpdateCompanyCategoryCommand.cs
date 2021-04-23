using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Commands.UpdateCompanyCategory
{
    public class UpdateCompanyCategoryCommand : IRequest<CompanyCategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SuperCompanyCategoryId { get; set; }
        public int[] CompanyIds { get; set; }


        public class Handler : IRequestHandler<UpdateCompanyCategoryCommand, CompanyCategoryDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CompanyCategoryDto> Handle(UpdateCompanyCategoryCommand request, CancellationToken cancellationToken)
            {
                CompanyCategory superCategory = null;
                List<Company> companies = null;

                var entity = await _context.CompanyCategories
                    .Include(x => x.Companies)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Company), request.Id);
                }

                if (entity.SuperCompanyCategoryId != request.SuperCompanyCategoryId &&
                    request.SuperCompanyCategoryId != null)
                {
                     superCategory = await _context.CompanyCategories
                        .FirstOrDefaultAsync(x => x.Id == request.SuperCompanyCategoryId, cancellationToken);

                    if (superCategory == null)
                        throw new NotFoundException(nameof(CompanyCategory), request.Id);
                }

                if (request.CompanyIds != null 
                    && request.CompanyIds.Any()
                    && request.CompanyIds != entity.Companies.Select(x => x.Id))
                {
                    companies = await _context.Companies
                        .Where(x => request.CompanyIds.Contains(x.Id))
                        .ToListAsync();

                    if (companies == null && companies.Count != request.CompanyIds.Length)
                        throw new NotFoundException(nameof(Company), request.CompanyIds);
                }

                entity.Name = request.Name;
                entity.SuperCompanyCategoryId = superCategory?.Id;
                entity.Companies = companies;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map< CompanyCategory,CompanyCategoryDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
