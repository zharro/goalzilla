using System;
using AutoMapper;
using Goalzilla.Goalzilla.Application.Common.Mapping;
using Goalzilla.Goalzilla.Application.Common.ViewModels;
using Goalzilla.Goalzilla.Domain;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Goalzilla.Goalzilla.Application.Events.ViewModels
{
    public class EventViewModel : IMapFrom<Event>
    {
        public Guid EventId { get; set; }
        public UserViewModel Creator { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime BeginsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public string Title { get; set; }
        public RsvpViewModel[] Rsvps { get; set; }
    }
}