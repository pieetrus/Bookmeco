using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommand : IRequest<ServiceCategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prize { get; set; }
        public int ServiceDuration { get; set; }

        public class Handler : IRequestHandler<UpdateServiceCategoryCommand, ServiceCategoryDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ServiceCategoryDto> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ServiceCategories.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ServiceCategory), request.Id);
                }

                entity.Name = request.Name;
                entity.Prize = request.Prize;
                entity.ServiceDuration = request.ServiceDuration;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ServiceCategory, ServiceCategoryDto>(entity);
            }
        }
    }
}
