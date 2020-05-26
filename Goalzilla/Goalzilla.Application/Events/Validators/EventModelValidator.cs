using FluentValidation;
using Goalzilla.Goalzilla.Application.Events.Models;
using Goalzilla.Goalzilla.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Application.Events.Validators
{
    public class EventModelValidator : AbstractValidator<EventModel>
    {
        public EventModelValidator(GoalzillaDbContext dbContext)
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .Length(1, 128);
            
            RuleFor(e => e.CreatorId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .MustAsync(async (creatorId, ct) =>
                    await dbContext.Users.AnyAsync(u => u.UserId == creatorId, ct))
                .WithMessage((@event, creatorId) => $"User with Id '{creatorId}' not found");
        }
    }
}