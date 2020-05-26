using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Goalzilla.Goalzilla.Domain
{
    public class Email : ValueObject
    {
        private Email(){}
        private Email(string email)
        {
            Value = email;
        }
        
        public string Value { get; private set; }
        
        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                return Result.Failure<Email>("Wrong email format");
            }
            return Result.Success(new Email(email));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static implicit operator string (Email email)
        {
            return email.Value;
        }
    }
}