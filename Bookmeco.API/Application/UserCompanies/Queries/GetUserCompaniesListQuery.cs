using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanies.Queries
{
    public class GetUserCompaniesListQuery : IRequest<IEnumerable<UserCompanyDto>>
    {
    }

    public class GetUserCompaniesListQueryHandler : IRequestHandler<GetUserCompaniesListQuery, IEnumerable<UserCompanyDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetUserCompaniesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserCompanyDto>> Handle(GetUserCompaniesListQuery request, CancellationToken cancellationToken)
        {
            var userCompanies = await _context.UserCompanies
                .Include(x => x.User)
                .Include(x => x.AccessType)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserCompany>, IEnumerable<UserCompanyDto>>(userCompanies);
        }
    }
}
