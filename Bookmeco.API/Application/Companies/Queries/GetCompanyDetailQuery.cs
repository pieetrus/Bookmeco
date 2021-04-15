using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Queries
{
    public class GetCompanyDetailQuery : IRequest<CompanyDto>
    {
        public int Id { get; set; }
    }

    public class GetCompanyDetailQueryHandler : IRequestHandler<GetCompanyDetailQuery, CompanyDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompanyDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompanyDto> Handle(GetCompanyDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Companies
                .Include(x => x.Contents)
                .Include(x => x.UserCompanies).ThenInclude(x => x.User)
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            return _mapper.Map<Company, CompanyDto>(entity);
        }
    }
}
