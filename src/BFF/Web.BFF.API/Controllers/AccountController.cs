using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.BFF.API.Application.Commands;
using Web.BFF.API.Services.Models;

namespace Web.BFF.API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Open an account, with or without initial amount.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(OpenAccountServiceResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> OpenAccount([FromBody] OpenAccountCommand command)
    {
        var accountId = await _mediator.Send(command);
        return Ok(new OpenAccountServiceResponse(accountId));
    }
}