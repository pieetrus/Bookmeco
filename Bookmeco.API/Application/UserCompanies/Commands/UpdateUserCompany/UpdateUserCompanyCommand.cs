using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanies.Commands.UpdateUserCompany
{
    public class UpdateUserCompanyCommand : IRequest<UserCompanyDto>
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CompanyId { get; set; }
        public int? AccessTypeId { get; set; }

        public class Handler : IRequestHandler<UpdateUserCompanyCommand, UserCompanyDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            public async Task<UserCompanyDto> Handle(UpdateUserCompanyCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.UserCompanies.FindAsync(request.Id);

                if (entity == null)
                    throw new NotFoundException(nameof(UserCompany), request.Id);

                if (request.UserId != null && !await _context.Users.AnyAsync(x => x.Id == request.UserId))
                    throw new NotFoundException(nameof(User), request.UserId);

                if (request.CompanyId != null && !await _context.Companies.AnyAsync(x => x.Id == request.CompanyId))
                    throw new NotFoundException(nameof(Company), request.CompanyId);

                if (request.CompanyId != null && !await _context.UserCompanyAccessTypes.AnyAsync(x => x.Id == request.AccessTypeId))
                    throw new NotFoundException(nameof(UserCompanyAccessType), request.AccessTypeId);

                entity.UserId = request.UserId ?? entity.UserId;
                entity.CompanyId = request.CompanyId ?? entity.CompanyId;
                entity.AccessTypeId = request.AccessTypeId ?? entity.AccessTypeId;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<UserCompany, UserCompanyDto>(entity);
            }
        }
    }
}
