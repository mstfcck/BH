using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.BFF.API.Application.Queries;
using Web.BFF.API.Models;

namespace Web.BFF.API.Controllers;

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
    /// Get user, accounts, and transactions by User Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(GetUserAccountTransactionQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] int userId)
    {
        var response = await _mediator.Send(new GetUserAccountTransactionQuery(userId));
        return Ok(response);
    }
}