using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.AssignServiceCategories
{
    public class AssignServiceCategoriesCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
        public List<int> ServiceCategoryIds { get; set; }

        public class Handler : IRequestHandler<AssignServiceCategoriesCommand, UserDto>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(AssignServiceCategoriesCommand request, CancellationToken cancellationToken)
            {
                List<ServiceCategory> serviceCategoriesDb = null;

                var user = await _context.Users
                    .Include(x => x.ServiceCategories)
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (user == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                if (request.ServiceCategoryIds != null && request.ServiceCategoryIds.Any())
                {
                    serviceCategoriesDb = await _context.ServiceCategories
                       .Where(x => request.ServiceCategoryIds.Contains(x.Id))
                       .ToListAsync(cancellationToken);

                    if (serviceCategoriesDb.Count != request.ServiceCategoryIds.Count)
                    {
                        throw new NotFoundException(nameof(ServiceCategory), "Some of objects on list was not found");
                    }
                }

                user.ServiceCategories = serviceCategoriesDb;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<User, UserDto>(user);
            }
        }
    }
}
