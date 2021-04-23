using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Commands.CreateUserCompanyAccessType
{
    public class CreateUserCompanyAccessTypeCommand : IRequest<UserCompanyAccessTypeDto>
    {
        public int AccessLevel { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateUserCompanyAccessTypeCommand, UserCompanyAccessTypeDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserCompanyAccessTypeDto> Handle(CreateUserCompanyAccessTypeCommand request, CancellationToken cancellationToken)
            {

                var entity = new UserCompanyAccessType
                {
                    Name = request.Name,
                    AccessLevel = request.AccessLevel
                };

                _context.UserCompanyAccessTypes.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map<UserCompanyAccessType, UserCompanyAccessTypeDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
