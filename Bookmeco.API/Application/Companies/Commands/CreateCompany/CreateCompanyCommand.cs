using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<CompanyDto>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public class Handler : IRequestHandler<CreateCompanyCommand, CompanyDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
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

                if (success) return _mapper.Map<Company, CompanyDto>(entity);

                throw new Exception("Problem saving changes");
            }
        }

    }
}
