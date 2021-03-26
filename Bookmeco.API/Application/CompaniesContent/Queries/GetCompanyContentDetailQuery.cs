using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompaniesContent.Queries
{
    public class GetCompanyContentDetailQuery : IRequest<CompanyContentDto>
    {
        public int Id { get; set; }
    }

    public class GetCompanyContentDetailQueryHandler : IRequestHandler<GetCompanyContentDetailQuery, CompanyContentDto>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompanyContentDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyContentDto> Handle(GetCompanyContentDetailQuery request, CancellationToken cancellationToken)
        {
            var companyContent = await _context.CompanyContents
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<CompanyContent, CompanyContentDto>(companyContent);
        }
    }
}
