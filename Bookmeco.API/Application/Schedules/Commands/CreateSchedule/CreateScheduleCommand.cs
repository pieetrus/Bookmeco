using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.Commands.CreateSchedule
{

    public class CreateScheduleCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public bool IsAvailable { get; set; }

        public class Handler : IRequestHandler<CreateScheduleCommand, int>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
            {

                var entity = new Schedule
                {
                    UserId = request.UserId,
                    IsAvailable = request.IsAvailable
                };

                _context.Schedules.Add(entity);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return entity.Id;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
