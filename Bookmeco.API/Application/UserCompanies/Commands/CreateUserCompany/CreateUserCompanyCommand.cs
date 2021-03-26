using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanies.Commands.CreateUserCompany
{
    public class CreateUserCompanyCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int AccessTypeId { get; set; }

        public class Handler : IRequestHandler<CreateUserCompanyCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateUserCompanyCommand request, CancellationToken cancellationToken)
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

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
