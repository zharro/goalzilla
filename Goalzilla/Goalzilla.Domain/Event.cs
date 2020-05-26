using System;
using System.Collections.Generic;

namespace Goalzilla.Goalzilla.Domain
{
    public class Event
    {
        // Constructor for EF.
        private Event()
        { }
        
        public Event(Guid eventId,
            User creator,
            DateTime beginsAt,
            DateTime endsAt,
            string title)
        {
            EventId = eventId;
            Creator = creator;
            CreatedAt = DateTime.UtcNow;
            BeginsAt = beginsAt;
            EndsAt = endsAt;
            Title = title;
            Rsvps = new HashSet<Rsvp>();
        }
        
        public Guid EventId { get; private set; }
        public User Creator { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime BeginsAt { get; private set; }
        public DateTime EndsAt { get; private set; }
        public string Title { get; private set; }
        public ISet<Rsvp> Rsvps { get; private set; }

        public void AddRsvp(Rsvp newRsvp)
        {
            Rsvps.Add(newRsvp);
        }
    }
}