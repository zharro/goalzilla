using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Goalzilla.Goalzilla.Application.Events.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Goalzilla.Goalzilla.Application.Events
{
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Creates a new event.
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wrong data")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Event has been created")]
        [HttpPost("")]
        public async Task<IActionResult> Create(EventCreate.Command command)
        {
            var newEventId = Guid.NewGuid();
            command.EventId = newEventId;
            await _mediator.Send(command);

            Maybe<EventViewModel> createdEvent = await _mediator.Send(new EventGetById.Query
            {
                EventId = newEventId
            });
            return CreatedAtAction(nameof(GetById), new {eventId = newEventId}, createdEvent.Value);
        }

        /// <summary>
        /// Gets an event by Id.
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Event with specified Id not found")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById(EventGetById.Query query)
        {
            Maybe<EventViewModel> eventVm = await _mediator.Send(query);
            if (eventVm.HasNoValue)
            {
                return NotFound();
            }
            return Ok(eventVm.Value);
        }
        
        /// <summary>
        /// Rsvp to an event.
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wrong data")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Rsvp has been saved")]
        [HttpPost("{eventId}/rsvp")]
        public async Task<IActionResult> Create(EventRsvp.Command command)
        {
            command.RsvpId = Guid.NewGuid();
            await _mediator.Send(command);
            Maybe<EventViewModel> updatedEvent = await _mediator.Send(new EventGetById.Query
            {
                EventId = command.EventId
            });
            return CreatedAtAction(nameof(GetById), new {eventId = command.EventId}, updatedEvent.Value);
        }
    }
}