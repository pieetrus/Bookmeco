using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Queries
{
    public class GetCompanyCategoriesListQuery : IRequest<IEnumerable<CompanyCategoryDto>>
    {
    }

    public class GetCompanyCategoriesListQueryHandler : IRequestHandler<GetCompanyCategoriesListQuery, IEnumerable<CompanyCategoryDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompanyCategoriesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyCategoryDto>> Handle(GetCompanyCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var companies = await _context.CompanyCategories
                .Include(x => x.Companies).ThenInclude(x => x.UserCompanies).ThenInclude(x => x.User)
                .Include(x => x.Companies).ThenInclude(x => x.Contents)
                .Include(x => x.SuperCompanyCategory)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CompanyCategory>, IEnumerable<CompanyCategoryDto>>(companies);
        }
    }
}
