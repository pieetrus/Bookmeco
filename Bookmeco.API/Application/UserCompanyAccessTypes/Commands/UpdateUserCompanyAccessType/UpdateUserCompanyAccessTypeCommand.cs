using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Commands.UpdateUserCompanyAccessType
{
    public class UpdateUserCompanyAccessTypeCommand : IRequest<UserCompanyAccessTypeDto>
    {
        public int Id { get; set; }
        public int AccessLevel { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<UpdateUserCompanyAccessTypeCommand, UserCompanyAccessTypeDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserCompanyAccessTypeDto> Handle(UpdateUserCompanyAccessTypeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.UserCompanyAccessTypes.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(UserCompanyAccessTypes), request.Id);
                }

                entity.AccessLevel = request.AccessLevel;
                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<UserCompanyAccessType, UserCompanyAccessTypeDto>(entity);
            }
        }
    }
}
