using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PersonsData.Queries
{
    public class GetPersonsDataListQuery : IRequest<IEnumerable<PersonDataDto>>
    {
    }

    public class GetPersonsDataListQueryHandler : IRequestHandler<GetPersonsDataListQuery, IEnumerable<PersonDataDto>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public GetPersonsDataListQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDataDto>> Handle(GetPersonsDataListQuery request, CancellationToken cancellationToken)
        {
            var personsData = await _context.PersonsData
                .Include(x => x.User)
                .ToListAsync(cancellationToken);


            return _mapper.Map<IEnumerable<PersonData>, IEnumerable<PersonDataDto>>(personsData);
        }
    }
}
