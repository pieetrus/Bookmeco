using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Commands.CreateUserCompanyAccessType
{
    public class CreateUserCompanyAccessTypeCommand : IRequest<int>
    {
        public int AccessLevel { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateUserCompanyAccessTypeCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateUserCompanyAccessTypeCommand request, CancellationToken cancellationToken)
            {

                var entity = new UserCompanyAccessType
                {
                    Name = request.Name,
                    AccessLevel = request.AccessLevel
                };

                _context.UserCompanyAccessTypes.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
