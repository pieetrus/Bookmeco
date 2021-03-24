using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AccessLevel { get; set; }

        public class Handler : IRequestHandler<UpdateRoleCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Roles.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Opinion), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.AccessLevel = request.AccessLevel ?? entity.AccessLevel;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
