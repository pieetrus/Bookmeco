using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Queries
{
    public class GetUserCompanyAccessTypesListQuery : IRequest<IEnumerable<UserCompanyAccessTypeDto>>
    {
    }

    public class GetUserCompanyAccessTypesListQueryHandler : IRequestHandler<GetUserCompanyAccessTypesListQuery, IEnumerable<UserCompanyAccessTypeDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetUserCompanyAccessTypesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserCompanyAccessTypeDto>> Handle(GetUserCompanyAccessTypesListQuery request, CancellationToken cancellationToken)
        {
            var userCompanies = await _context.UserCompanyAccessTypes
                .Include(x => x.UserCompanies)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserCompanyAccessType>, IEnumerable<UserCompanyAccessTypeDto>>(userCompanies);
        }
    }
}
