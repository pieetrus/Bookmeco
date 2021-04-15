using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
                .Include(x => x.Contents)
                .Include(x => x.UserCompanies).ThenInclude(x => x.User)
                .Include(x => x.Categories)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(companies);
        }
    }

}
