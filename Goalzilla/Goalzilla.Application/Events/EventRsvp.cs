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
    public class EventRsvp
    {
        public class Command : IRequest
        {
            [BindNever]
            public Guid RsvpId { get; set; }
            
            [FromRoute(Name = "eventId")]
            public Guid EventId { get; set; }

            [Required]
            [FromBody]
            public RsvpModel Body { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(GoalzillaDbContext dbContext)
            {
                RuleFor(c => c.Body)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .SetValidator(new RsvpModelValidator(dbContext));
                
                RuleFor(c => c.EventId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .MustAsync(async (eventId, ct) =>
                        await dbContext.Events.AnyAsync(e => e.EventId == eventId, ct))
                    .WithMessage((command, eventId) => $"Event with Id '{eventId}' not found");
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
                var @event = await _dbContext.Events
                    .Include(e => e.Rsvps)
                    .SingleOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken: cancellationToken);
                var attendee = await _dbContext.Users
                    .SingleOrDefaultAsync(u => u.UserId == request.Body.AttendeeId, cancellationToken: cancellationToken);

                var newRsvp = new Rsvp(request.RsvpId, attendee, request.Body.IsAccepted);
                @event.AddRsvp(newRsvp);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}