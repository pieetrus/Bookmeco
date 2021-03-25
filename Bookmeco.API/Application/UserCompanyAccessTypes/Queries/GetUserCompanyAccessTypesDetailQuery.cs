using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Queries
{
    public class GetUserCompanyAccessTypesDetailQuery : IRequest<UserCompanyAccessTypeDto>
    {
        public int Id { get; set; }
    }

    public class GetUserCompanyAccessTypesDetailQueryHandler : IRequestHandler<GetUserCompanyAccessTypesDetailQuery, UserCompanyAccessTypeDto>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetUserCompanyAccessTypesDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserCompanyAccessTypeDto> Handle(GetUserCompanyAccessTypesDetailQuery request, CancellationToken cancellationToken)
        {
            var userCompanyAccessType = await _context.UserCompanyAccessTypes
                .Include(x => x.UserCompanies)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (userCompanyAccessType == null)
                throw new NotFoundException(nameof(UserCompanyAccessType), request.Id);

            return _mapper.Map<UserCompanyAccessType, UserCompanyAccessTypeDto>(userCompanyAccessType);
        }
    }
}
