﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public List<int> ServiceCategoryIds { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                List<ServiceCategory> serviceCategoriesDb = null;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

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

                user.UserName = request.Username ?? user.UserName;
                user.FirstName = request.FirstName ?? user.FirstName;
                user.LastName = request.LastName ?? user.LastName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.Email = request.Email ?? user.Email;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
