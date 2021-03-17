using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


        public class Handler : IRequestHandler<UpdateCompanyCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Companies.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Company), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.Address = request.Address ?? entity.Address;
                entity.Latitude = request.Latitude ?? entity.Latitude;
                entity.Longitude = request.Longitude ?? entity.Longitude;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
