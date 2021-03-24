using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Queries
{
    public class GetServiceCategoriesListQuery : IRequest<IEnumerable<ServiceCategoryDto>>
    {
    }

    public class GetServiceCategoriesListQueryHandler : IRequestHandler<GetServiceCategoriesListQuery, IEnumerable<ServiceCategoryDto>>
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetServiceCategoriesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceCategoryDto>> Handle(GetServiceCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var serviceCategories = await _context.ServiceCategories
                .Include(x => x.Reservations)
                .Include(x => x.Users)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ServiceCategory>, IEnumerable<ServiceCategoryDto>>(serviceCategories);
        }
    }
}
