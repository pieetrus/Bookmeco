using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<CompanyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public class Handler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Companies
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Company), request.Id);
                }

                entity.Name = request.Name;
                entity.Address = request.Address;
                entity.Latitude = request.Latitude;
                entity.Longitude = request.Longitude;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Company, CompanyDto>(entity);

            }
        }
    }
}
