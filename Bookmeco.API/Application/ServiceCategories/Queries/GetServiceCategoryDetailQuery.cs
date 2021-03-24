using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Queries
{

    public class GetServiceCategoryDetailQuery : IRequest<ServiceCategoryDto>
    {
        public int Id { get; set; }
    }

    public class GetServiceCategoryDetailQueryHandler : IRequestHandler<GetServiceCategoryDetailQuery, ServiceCategoryDto>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetServiceCategoryDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCategoryDto> Handle(GetServiceCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var serviceCategory = await _context.ServiceCategories
                .Include(x => x.Reservations)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<ServiceCategory, ServiceCategoryDto>(serviceCategory);
        }
    }
}
