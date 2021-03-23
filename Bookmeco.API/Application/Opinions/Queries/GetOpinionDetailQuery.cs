using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Opinions.Queries
{
    public class GetOpinionDetailQuery : IRequest<OpinionDto>
    {
        public int Id { get; set; }
    }

    public class GetOpinionDetailQueryHandler : IRequestHandler<GetOpinionDetailQuery, OpinionDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetOpinionDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OpinionDto> Handle(GetOpinionDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Opinions
                .Include(x => x.User)
                .Include(x => x.Reservation)
                .Include(x => x.SuperOpinion)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(OpinionDto), request.Id);
            }

            return _mapper.Map<Opinion, OpinionDto>(entity);
        }
    }
}
