using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Opinions.Queries
{
    public class GetOpinionsListQuery : IRequest<IEnumerable<OpinionDto>>
    {
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
            var opinions = await _context.Opinions
                .Include(x => x.User)
                .Include(x => x.Reservation)
                .Include(x => x.SuperOpinion)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Opinion>, IEnumerable<OpinionDto>>(opinions);
        }
    }
}
