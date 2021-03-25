using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCompanyAccessTypes.Commands.UpdateUserCompanyAccessType
{
    public class UpdateUserCompanyAccessTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int? AccessLevel { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<UpdateUserCompanyAccessTypeCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateUserCompanyAccessTypeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.UserCompanyAccessTypes.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(UserCompanyAccessTypes), request.Id);
                }

                entity.AccessLevel = request.AccessLevel ?? entity.AccessLevel;
                entity.Name = request.Name ?? entity.Name;

                _context.UserCompanyAccessTypes.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
