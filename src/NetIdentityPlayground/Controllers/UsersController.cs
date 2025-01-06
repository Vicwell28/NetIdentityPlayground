using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetIdentityPlayground.Application.Common.DTOs;
using NetIdentityPlayground.Application.Features.Users.Commands;

namespace NetIdentityPlayground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<UserDto>>> RegisterUserAsync([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
