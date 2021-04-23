using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Commands.CreateCompanyCategory
{
    public class CreateCompanyCategoryCommand : IRequest<CompanyCategoryDto>
    {

        public string Name { get; set; }
        public int? SuperCompanyCategoryId { get; set; }
        public int[] CompanyIds { get; set; }

        public class Handler : IRequestHandler<CreateCompanyCategoryCommand, CompanyCategoryDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CompanyCategoryDto> Handle(CreateCompanyCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new CompanyCategory { Name = request.Name };

                if (request.SuperCompanyCategoryId != null)
                {
                    var superCompanyCategory = await _context.CompanyCategories.FindAsync(request.SuperCompanyCategoryId);

                    if (superCompanyCategory == null)
                        throw new NotFoundException(nameof(CompanyCategory), request.SuperCompanyCategoryId);

                    entity.SuperCompanyCategory = superCompanyCategory;
                }

                if (request.CompanyIds != null && request.CompanyIds.Any())
                {
                    var companies = await _context.Companies.Where(x => request.CompanyIds.Contains(x.Id)).ToListAsync();

                    if (companies == null ||
                        companies.Count != request.CompanyIds.Length)
                            throw new NotFoundException(nameof(Company), request.CompanyIds);

                    entity.Companies = companies;
                }

                _context.CompanyCategories.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map<CompanyCategory, CompanyCategoryDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
