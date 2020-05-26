using FluentValidation;
using Goalzilla.Goalzilla.Application.Events.Models;
using Goalzilla.Goalzilla.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Application.Events.Validators
{
    public class RsvpModelValidator : AbstractValidator<RsvpModel>
    {
        public RsvpModelValidator(GoalzillaDbContext dbContext)
        {
            RuleFor(rsvp => rsvp.AttendeeId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .MustAsync(async (attendeeId, ct) =>
                    await dbContext.Users.AnyAsync(u => u.UserId == attendeeId, ct))
                .WithMessage((rsvp, attendeeId) => $"User with Id '{attendeeId}' not found");
        }
    }
}