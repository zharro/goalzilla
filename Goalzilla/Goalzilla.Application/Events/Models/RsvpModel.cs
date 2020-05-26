using System;

namespace Goalzilla.Goalzilla.Application.Events.Models
{
    public class RsvpModel
    {
        public Guid AttendeeId { get; set; }
        public bool IsAccepted { get; set; }
    }
}