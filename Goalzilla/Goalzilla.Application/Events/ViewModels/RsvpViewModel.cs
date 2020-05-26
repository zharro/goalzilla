using System;
using Goalzilla.Goalzilla.Application.Common.Mapping;
using Goalzilla.Goalzilla.Application.Common.ViewModels;
using Goalzilla.Goalzilla.Domain;

namespace Goalzilla.Goalzilla.Application.Events.ViewModels
{
    public class RsvpViewModel : IMapFrom<Rsvp>
    {
        public UserViewModel Attendee { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}