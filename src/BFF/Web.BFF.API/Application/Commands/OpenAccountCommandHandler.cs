using MediatR;
using Shared.Exceptions;
using Web.BFF.API.Services;
using Web.BFF.API.Services.Models;

namespace Web.BFF.API.Application.Commands;

public class OpenAccountCommandHandler : IRequestHandler<OpenAccountCommand, int>
{
    private readonly IAccountServiceHttpClient _accountService;
    private readonly ITransactionServiceHttpClient _transactionService;
    private readonly IUserServiceHttpClient _userService;
    
    public OpenAccountCommandHandler(
        IAccountServiceHttpClient accountService,
        ITransactionServiceHttpClient transactionService, 
        IUserServiceHttpClient userService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
        _userService = userService;
    }

    public async Task<int> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUser(request.CustomerId);

        if (user == null)
        {
            throw new BFFException("The requested user couldn't be found.");
        }
        
        var createdAccount = await _accountService.OpenAccount(new OpenAccountServiceRequest
        {
            CustomerId = request.CustomerId
        });

        if (request.Amount > 0)
        {
            var y = await _transactionService.CreateTransaction(new CreateTransactionServiceRequest
            {
                AccountId = createdAccount.AccountId,
                Amount = request.Amount,
                TransactionType = (int)request.TransactionType
            });
        }

        return createdAccount.AccountId;
    }
}