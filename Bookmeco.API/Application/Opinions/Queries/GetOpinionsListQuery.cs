using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Opinions.Queries
{
    public class GetOpinionsListQuery : IRequest<IEnumerable<OpinionDto>>
    {
        public int? ReservationId { get; set; }
    }

    public class GetCompanyCategoriesListQueryHandler : IRequestHandler<GetOpinionsListQuery, IEnumerable<OpinionDto>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetCompanyCategoriesListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OpinionDto>> Handle(GetOpinionsListQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Opinions.AsQueryable();

            if (request.ReservationId != null)
            {
                queryable = queryable.Where(x => x.ReservationId == request.ReservationId);
            }

            var opinions = await queryable
                .Include(x => x.User)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Opinion>, IEnumerable<OpinionDto>>(opinions);
        }
    }
}
