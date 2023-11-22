using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Transaction.API.Models;

namespace Transaction.API.Application.Queries;

public class GetTransactionListQueryHandler : IRequestHandler<GetTransactionListQuery, GetTransactionListResponse>
{
    private readonly IMemoryCache _memoryCache;

    public GetTransactionListQueryHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<GetTransactionListResponse> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
    {
        _memoryCache.TryGetValue(request.AccountId, out List<Domain.Aggregates.TransactionAggregate.Transaction>? transactions);

        var transactionList = new GetTransactionListResponse();
        
        if (transactions != null)
        {
            transactionList.Transactions = transactions.Select(x => new GetTransactionResponse(x.Id, x.Amount, (int)x.Type)).ToList();
        }

        return await Task.FromResult(transactionList);
    }
}