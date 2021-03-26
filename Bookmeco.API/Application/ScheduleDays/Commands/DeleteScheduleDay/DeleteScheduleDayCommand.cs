using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ScheduleDays.Commands.DeleteScheduleDay
{
    public class DeleteScheduleDayCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteScheduleDayCommandHandler : IRequestHandler<DeleteScheduleDayCommand>
    {
        private readonly IDataContext _context;

        public DeleteScheduleDayCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteScheduleDayCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ScheduleDays.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ScheduleDay), request.Id);
            }

            _context.ScheduleDays.Remove(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");
        }
    }
}
