using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Goalzilla.Goalzilla.Application.Events.Models;
using Goalzilla.Goalzilla.Application.Events.Validators;
using Goalzilla.Goalzilla.Domain;
using Goalzilla.Goalzilla.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Application.Events
{
    public class EventCreate
    {
        public class Command : IRequest
        {
            [BindNever]
            public Guid EventId { get; set; }

            [Required]
            [FromBody]
            public EventModel Body { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(GoalzillaDbContext dbContext)
            {
                RuleFor(c => c.Body)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .SetValidator(new EventModelValidator(dbContext));
            }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly GoalzillaDbContext _dbContext;

            public Handler(GoalzillaDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }
            
            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await Task.Delay(2000, cancellationToken);
                
                var creator = await _dbContext.Users
                    .SingleOrDefaultAsync(u => u.UserId == request.Body.CreatorId, cancellationToken: cancellationToken);

                var newEvent = new Event(request.EventId,
                    creator,
                    request.Body.BeginsAt,
                    request.Body.EndsAt,
                    request.Body.Title);

                _dbContext.Events.Add(newEvent);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}