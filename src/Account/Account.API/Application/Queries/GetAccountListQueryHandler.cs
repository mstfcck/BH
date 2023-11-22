using Account.API.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Account.API.Application.Queries;

public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, GetAccountListResponse>
{
    private readonly IMemoryCache _memoryCache;

    public GetAccountListQueryHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<GetAccountListResponse> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
    {
        _memoryCache.TryGetValue(request.CustomerId, out List<Domain.Aggregates.AccountAggregate.Account>? accounts);

        var accountList = new GetAccountListResponse();
        
        if (accounts != null)
        {
            accountList.Accounts = accounts.Select(x => new GetAccountResponse(x.Id));
        }

        return await Task.FromResult(accountList);
    }
}