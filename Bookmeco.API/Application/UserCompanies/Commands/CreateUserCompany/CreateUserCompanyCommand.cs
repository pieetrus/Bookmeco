using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanies.Commands.CreateUserCompany
{
    public class CreateUserCompanyCommand : IRequest<UserCompanyDto>
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int AccessTypeId { get; set; }

        public class Handler : IRequestHandler<CreateUserCompanyCommand, UserCompanyDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserCompanyDto> Handle(CreateUserCompanyCommand request, CancellationToken cancellationToken)
            {

                if (!await _context.Users.AnyAsync(x => x.Id == request.UserId))
                    throw new NotFoundException(nameof(User), request.UserId);

                if (!await _context.Companies.AnyAsync(x => x.Id == request.CompanyId))
                    throw new NotFoundException(nameof(Company), request.CompanyId);

                if (!await _context.UserCompanyAccessTypes.AnyAsync(x => x.Id == request.AccessTypeId))
                    throw new NotFoundException(nameof(UserCompanyAccessType), request.AccessTypeId);

                var entity = new UserCompany
                {
                    UserId = request.UserId,
                    CompanyId = request.CompanyId,
                    AccessTypeId = request.AccessTypeId
                };

                _context.UserCompanies.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                var accessType =
                    await _context.UserCompanyAccessTypes.FirstOrDefaultAsync(x => x.Id == entity.AccessTypeId,
                        cancellationToken);

                entity.AccessType = accessType;

                if (success) return _mapper.Map<UserCompany, UserCompanyDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
