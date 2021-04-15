using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Commands.UpdateSchedule
{
    public class UpdateScheduleCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? IsAvailable { get; set; }

        public class Handler : IRequestHandler<UpdateScheduleCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }


            public async Task<Unit> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Schedules.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Schedule), request.Id);
                }

                entity.UserId = request.UserId ?? entity.UserId;
                entity.IsAvailable = request.IsAvailable ?? entity.IsAvailable;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
