using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public float Prize { get; set; }
        public int ServiceDuration { get; set; }

        public class Handler : IRequestHandler<CreateServiceCategoryCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
            {

                var entity = new ServiceCategory
                {
                    Name = request.Name,
                    ServiceDuration = request.ServiceDuration,
                    Prize = request.Prize
                };

                _context.ServiceCategories.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
