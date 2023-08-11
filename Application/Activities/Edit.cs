using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);
                
                // sets Title in activity to the value received in request if it's not null
                // otherwise leaves it untouched
                activity.Title = request.Activity.Title ?? activity.Title;

                await _context.SaveChangesAsync();

                return Unit.Value;
            }

        }

    }
}