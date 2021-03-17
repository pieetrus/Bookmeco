using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Companies.Queries
{
    public class GetCompaniesListQuery : IRequest<IEnumerable<CompanyDto>>
    {
    }

    public class GetCompaniesListQueryHandler : IRequestHandler<GetCompaniesListQuery, IEnumerable<CompanyDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompaniesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesListQuery request, CancellationToken cancellationToken)
        {
            var companies = await _context.Companies
                .Include(x => x.Content)
                .Include(x => x.Users)
                .Include(x => x.Categories)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }
    }

}
