using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public class Handler : IRequestHandler<CreateCompanyCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
            {
                var entity = new Company
                {
                    Name = request.Name,
                    Address = request.Address,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                };

                _context.Companies.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
