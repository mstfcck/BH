using Microsoft.Extensions.Caching.Memory;
using AccountEntity = Account.Domain.Aggregates.AccountAggregate.Account;

namespace Account.API;

public static class InMemoryFakeDb
{
    public static void UseFakeAccountData(this IServiceProvider services)
    {
        var memoryCache = services.GetService<IMemoryCache>();

        var userId = 1000001; // Customer Id
        
        var initialAccounts = new List<AccountEntity>
        {
            new()
            {
                Id = 2000001, // Account Id
                CustomerId = userId
            }
        };

        foreach (AccountEntity account in initialAccounts)
        {
            if (!memoryCache.TryGetValue(userId, out List<AccountEntity>? accounts))
            {
                accounts = new List<AccountEntity>();
                
                memoryCache.Set(userId, accounts, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(15)
                });
            }

            accounts?.Add(account);
        }
    }
}