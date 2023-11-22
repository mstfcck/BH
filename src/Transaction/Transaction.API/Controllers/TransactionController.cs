using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.API.Application.Commands;
using Transaction.API.Application.Queries;
using Transaction.API.Models;

namespace Transaction.API.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Create a transaction.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateTransactionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        var transactionId = await _mediator.Send(command);
        return Ok(new CreateTransactionResponse(transactionId));
    }

    /// <summary>
    /// Get transactions of account.
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet("account/{accountId:int}")]
    [ProducesResponseType(typeof(GetTransactionListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAccountTransactionList([FromRoute] int accountId)
    {
        var customerAccountList = await _mediator.Send(new GetTransactionListQuery(accountId));
        return Ok(new GetTransactionListResponse
        {
            Transactions = customerAccountList.Transactions.Select(x=> new GetTransactionResponse(x.TransactionId, x.Amount, x.TransactionType)).ToList()
        });
    }
}

