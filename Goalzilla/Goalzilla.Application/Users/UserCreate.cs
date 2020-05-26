using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Goalzilla.Goalzilla.Application.Users.Models;
using Goalzilla.Goalzilla.Application.Users.Validators;
using Goalzilla.Goalzilla.Domain;
using Goalzilla.Goalzilla.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Goalzilla.Goalzilla.Application.Users
{
    public class UserCreate
    {
        public class Command : IRequest
        {
            [BindNever]
            public Guid UserId { get; set; }

            [Required]
            [FromBody]
            public UserModel Body { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Body)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .SetValidator(new UserModelValidator());
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
                Result<Email> email = Email.Create(request.Body.Email);
                
                var newUser = new User(request.UserId,
                    request.Body.FirstName,
                    request.Body.LastName,
                    email.Value);

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}