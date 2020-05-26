using System;

namespace Goalzilla.Goalzilla.Application.Users.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}