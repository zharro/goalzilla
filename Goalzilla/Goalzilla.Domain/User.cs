using System;

namespace Goalzilla.Goalzilla.Domain
{
    public class User
    {
        // Constructor for EF.
        private User()
        { }
        
        public User(Guid userId, string firstName, string lastName, Email email)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }
    
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}