using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

    public class Handler : IRequestHandler<Command>
    {

        // DataContext needed here to persist the changes
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
            
        }

        // Unit returns "nothing" but is needed by API Controller to know that
        // the call has finished
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            // currently saved only to memory, so no need for async function call
            _context.Activities.Add(request.Activity);

            await _context.SaveChangesAsync();
            return Unit.Value;
        }


    }



    }
}