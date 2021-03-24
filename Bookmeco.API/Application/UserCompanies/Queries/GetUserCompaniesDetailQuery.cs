using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanies.Queries
{
    public class GetUserCompaniesDetailQuery : IRequest<UserCompanyDto>
    {
        public int Id { get; set; }
    }

    public class GetUserCompaniesDetailQueryHandler : IRequestHandler<GetUserCompaniesDetailQuery, UserCompanyDto>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetUserCompaniesDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserCompanyDto> Handle(GetUserCompaniesDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserCompanies
                .Include(x => x.User)
                .Include(x => x.AccessType)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(entity), request.Id);

            return _mapper.Map<UserCompany, UserCompanyDto>(entity);
        }
    }
}
