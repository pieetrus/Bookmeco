using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Prize { get; set; }
        public int? ServiceDuration { get; set; }

        public class Handler : IRequestHandler<UpdateServiceCategoryCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ServiceCategories.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ServiceCategory), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.Prize = request.Prize ?? entity.Prize;
                entity.ServiceDuration = request.ServiceDuration ?? entity.ServiceDuration;

                _context.ServiceCategories.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
