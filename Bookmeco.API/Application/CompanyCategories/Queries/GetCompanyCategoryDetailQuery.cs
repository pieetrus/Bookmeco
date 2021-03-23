using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompanyCategories.Queries
{

    public class GetCompanyCategoryDetailQuery : IRequest<CompanyCategoryDto>
    {
        public int Id { get; set; }
    }

    public class GetCompanyCategoryDetailQueryHandler : IRequestHandler<GetCompanyCategoryDetailQuery, CompanyCategoryDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompanyCategoryDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyCategoryDto> Handle(GetCompanyCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.CompanyCategories
                .Include(x => x.Companies)
                .Include(x => x.SuperCompanyCategory)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CompanyCategoryDto), request.Id);
            }

            return _mapper.Map<CompanyCategory, CompanyCategoryDto>(entity);
        }
    }
}
