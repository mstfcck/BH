using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Application.Commands;
using User.API.Application.Queries;
using User.API.Models;

namespace User.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a user.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new CreateUserResponse { UserId = userId });
    }

    /// <summary>
    /// Get user.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] int userId)
    {
        var user = await _mediator.Send(new GetUserQuery(userId));
        return Ok(new GetUserResponse(user.Id, user.Name, user.Surname));
    }
}