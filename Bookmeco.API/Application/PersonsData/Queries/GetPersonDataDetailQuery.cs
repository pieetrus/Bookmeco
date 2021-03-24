using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PersonsData.Queries
{
    public class GetPersonDataDetailQuery : IRequest<PersonDataDto>
    {
        public int Id { get; set; }
    }

    public class GetPersonDataDetailQueryHandler : IRequestHandler<GetPersonDataDetailQuery, PersonDataDto>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetPersonDataDetailQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PersonDataDto> Handle(GetPersonDataDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PersonsData
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PersonDataDto), request.Id);
            }

            return _mapper.Map<PersonData, PersonDataDto>(entity);
        }
    }
}
