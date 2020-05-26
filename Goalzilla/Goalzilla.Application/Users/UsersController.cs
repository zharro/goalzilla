using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Goalzilla.Goalzilla.Application.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Goalzilla.Goalzilla.Application.Users
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Creates a new user.
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wrong data")]
        [SwaggerResponse((int)HttpStatusCode.Created, "User has been created")]
        [HttpPost("")]
        public async Task<IActionResult> Create(UserCreate.Command command)
        {
            var newUserId = Guid.NewGuid();
            command.UserId = newUserId;
            
            await _mediator.Send(command);

            Maybe<UserViewModel> createdUser = await _mediator.Send(new UserGetById.Query
            {
                UserId = newUserId
            });

            return CreatedAtAction(nameof(GetById), new {userId = newUserId}, createdUser.Value);
        }
        
        /// <summary>
        /// Gets an user by Id.
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.NotFound, "User with specified Id not found")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(UserGetById.Query query)
        {
            Maybe<UserViewModel> userVm = await _mediator.Send(query);

            if (userVm.HasNoValue)
            {
                return NotFound();
            }
            return Ok(userVm.Value);
        }
    }
}