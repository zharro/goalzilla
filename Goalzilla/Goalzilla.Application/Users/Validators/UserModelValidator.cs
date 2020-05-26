using CSharpFunctionalExtensions;
using FluentValidation;
using Goalzilla.Goalzilla.Application.Users.Models;
using Goalzilla.Goalzilla.Domain;

namespace Goalzilla.Goalzilla.Application.Users.Validators
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(u => u.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(3, 128)
                .Must((user, email, ctx) =>
                {
                    Result<Email> result = Email.Create(email);

                    if (result.IsFailure)
                    {
                        ctx.MessageFormatter.AppendArgument("CodeValidationError", result.Error);
                    }
                    return result.IsSuccess;
                })
                .WithMessage("{CodeValidationError}");
            
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .Length(1, 128);
            
            RuleFor(u => u.LastName)
                .NotEmpty()
                .Length(1, 128);
        }
    }
}