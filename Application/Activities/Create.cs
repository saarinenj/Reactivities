using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;


namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {

            // DataContext needed here to persist the changes
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            // Unit returns "nothing" but is needed by API Controller to know that
            // the call has finished
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // currently saved only to memory, so no need for async function call
                _context.Activities.Add(request.Activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create activity");

                return Result<Unit>.Success(Unit.Value); 
            }


        }



    }
}