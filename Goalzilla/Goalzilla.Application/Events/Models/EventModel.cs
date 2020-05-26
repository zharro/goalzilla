using System;
using System.ComponentModel.DataAnnotations;

namespace Goalzilla.Goalzilla.Application.Events.Models
{
    public class EventModel
    {
        [Required]
        public Guid CreatorId { get;  set; }
        
        [Required]
        public DateTime BeginsAt { get; set; }
        
        [Required]
        public DateTime EndsAt { get; set; }
        
        [Required]
        public string Title { get; set; }
    }
}