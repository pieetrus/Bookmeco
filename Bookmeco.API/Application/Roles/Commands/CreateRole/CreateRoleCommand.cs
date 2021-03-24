using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int AccessLevel { get; set; }

        public class Handler : IRequestHandler<CreateRoleCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {

                var entity = new Role
                {
                    Name = request.Name,
                    AccessLevel = request.AccessLevel
                };

                _context.Roles.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
