using Account.API.Application.Commands;
using Account.API.Application.Queries;
using Account.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers;

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
    /// Open an account.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(OpenAccountResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> OpenAccount([FromBody] OpenAccountCommand command)
    {
        var accountId = await _mediator.Send(command);
        return Ok(new OpenAccountResponse(accountId));
    }
    
    /// <summary>
    /// Get accounts of customer.
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("customer/{customerId:int}")]
    [ProducesResponseType(typeof(GetAccountListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerAccountList([FromRoute] int customerId)
    {
        var customerAccountList = await _mediator.Send(new GetAccountListQuery(customerId));
        return Ok(new GetAccountListResponse
        {
            Accounts = customerAccountList.Accounts.Select(x=> new GetAccountResponse(x.AccountId)).ToList()
        });
    }
}