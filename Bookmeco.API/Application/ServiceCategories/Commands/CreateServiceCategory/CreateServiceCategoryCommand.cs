using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommand : IRequest<ServiceCategoryDto>
    {
        public string Name { get; set; }
        public float Prize { get; set; }
        public int ServiceDuration { get; set; }

        public class Handler : IRequestHandler<CreateServiceCategoryCommand, ServiceCategoryDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ServiceCategoryDto> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new ServiceCategory
                {
                    Name = request.Name,
                    ServiceDuration = request.ServiceDuration,
                    Prize = request.Prize
                };

                _context.ServiceCategories.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return _mapper.Map<ServiceCategory, ServiceCategoryDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
