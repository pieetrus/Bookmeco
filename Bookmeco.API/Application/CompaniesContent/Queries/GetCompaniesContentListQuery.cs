using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Queries
{
    public class GetCompaniesContentListQuery : IRequest<IEnumerable<CompanyContentDto>>
    {
    }

    public class GetCompaniesContentListQueryHandler : IRequestHandler<GetCompaniesContentListQuery, IEnumerable<CompanyContentDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompaniesContentListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyContentDto>> Handle(GetCompaniesContentListQuery request, CancellationToken cancellationToken)
        {
            var companiesContent = await _context.CompanyContents
                .Include(x => x.Company)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CompanyContent>, IEnumerable<CompanyContentDto>>(companiesContent);
        }
    }
}
