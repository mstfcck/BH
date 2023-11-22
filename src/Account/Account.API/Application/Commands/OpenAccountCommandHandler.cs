using MediatR;
using Microsoft.Extensions.Caching.Memory;
using AccountEntity = Account.Domain.Aggregates.AccountAggregate.Account;

namespace Account.API.Application.Commands;

public class OpenAccountCommandHandler : IRequestHandler<OpenAccountCommand, int>
{
    private readonly IMemoryCache _memoryCache;

    public OpenAccountCommandHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<int> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
    {
        if (!_memoryCache.TryGetValue(request.CustomerId, out List<AccountEntity>? accounts))
        {
            accounts = new List<AccountEntity>();
                
            _memoryCache.Set(request.CustomerId, accounts, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(15)
            });
        }

        var accountId  = new Random().Next(2000000, 2999999);

        var account = new AccountEntity
        {
            Id = accountId,
            CustomerId = request.CustomerId
        };

        accounts?.Add(account);

        return await Task.FromResult(accountId);
    }
}