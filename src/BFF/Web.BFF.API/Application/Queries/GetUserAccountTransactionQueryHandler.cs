using MediatR;
using Shared.Exceptions;
using Web.BFF.API.Models;
using Web.BFF.API.Services;
using Web.BFF.API.Services.Models;

namespace Web.BFF.API.Application.Queries;

public class GetUserAccountTransactionQueryHandler : IRequestHandler<GetUserAccountTransactionQuery, GetUserAccountTransactionQueryResponse>
{
    private readonly IAccountServiceHttpClient _accountService;
    private readonly ITransactionServiceHttpClient _transactionService;
    private readonly IUserServiceHttpClient _userService;

    public GetUserAccountTransactionQueryHandler(
        IAccountServiceHttpClient accountService,
        ITransactionServiceHttpClient transactionService,
        IUserServiceHttpClient userService
    )
    {
        _accountService = accountService;
        _transactionService = transactionService;
        _userService = userService;
    }

    public async Task<GetUserAccountTransactionQueryResponse> Handle(GetUserAccountTransactionQuery request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userService.GetUser(request.CustomerId);

        if (user == null)
        {
            throw new BFFException("User couldn't be found.");
        }
        
        // Get user's accounts
        var accountList = await _accountService.GetCustomerAccountList(user.Id);

        // Get user's accounts' transactions

        var transactionList = new Dictionary<int, GetTransactionListServiceResponse>();

        foreach (var account in accountList.Accounts)
        {
            var transaction = await _transactionService.GetAccountTransactionList(account.AccountId);
            transactionList.Add(account.AccountId, transaction);
        }
        
        // Mappings
        
        var response = new GetUserAccountTransactionQueryResponse
        {
            User =
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Accounts = accountList.Accounts.Select(x => new GetAccountQueryResponse
                {
                    AccountId = x.AccountId,
                    Transactions = transactionList[x.AccountId].Transactions.Select(y => new GetTransactionQueryResponse
                    {
                        TransactionId = y.TransactionId,
                        Amount = y.Amount,
                        TransactionType = (TransactionType)y.TransactionType
                    }).ToList()
                }).ToList()
            }
        };

        return response;
    }
}