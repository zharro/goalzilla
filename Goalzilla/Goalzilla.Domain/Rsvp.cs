using System;

namespace Goalzilla.Goalzilla.Domain
{
    // A reply of an invited user.
    public class Rsvp : IEquatable<Rsvp>
    {
        // Constructor for EF.
        private Rsvp()
        { }
        
        public Rsvp(Guid rsvpId, User attendee, bool isAccepted)
        {
            RsvpId = rsvpId;
            Attendee = attendee ?? throw new ArgumentNullException(nameof(attendee));
            IsAccepted = isAccepted;
            CreatedAt = DateTime.UtcNow;
        }
        
        public Guid RsvpId { get; private set; }
        public User Attendee { get; private set; }
        public bool IsAccepted { get; private set; }
        public DateTime CreatedAt { get; private set; }

        #region Implementation of IEquatable

        public bool Equals(Rsvp other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return RsvpId.Equals(other.RsvpId);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Rsvp);
        }

        public override int GetHashCode()
        {
            return RsvpId.GetHashCode();
        }
        
        #endregion
    }
}