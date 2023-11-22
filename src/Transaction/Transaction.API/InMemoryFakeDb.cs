using Microsoft.Extensions.Caching.Memory;
using Transaction.Domain.Aggregates.TransactionAggregate;
using TransactionEntity = Transaction.Domain.Aggregates.TransactionAggregate.Transaction;

namespace Transaction.API;

public static class InMemoryFakeDb
{
    public static void UseFakeTransactionData(this IServiceProvider services)
    {
        var memoryCache = services.GetService<IMemoryCache>();

        var accountId = 2000001; // Account Id of Customer (1000001)
        
        var initialTransactions = new List<TransactionEntity>
        {
            new()
            {
                Id = 3000001, // Transaction Id
                AccountId = accountId,
                Type = TransactionType.Deposit,
                Amount = 1000
            }
        };
        
        foreach (var transaction in initialTransactions)
        {
            if (!memoryCache.TryGetValue(accountId, out List<TransactionEntity>? transactions))
            {
                transactions = new List<TransactionEntity>();
                
                memoryCache.Set(accountId, transactions, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(15)
                });
            }

            transactions?.Add(transaction);
        }
    }
}