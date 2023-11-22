using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TransactionEntity = Transaction.Domain.Aggregates.TransactionAggregate.Transaction;

namespace Transaction.API.Application.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
{
    private readonly IMemoryCache _memoryCache;

    public CreateTransactionCommandHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        int transactionId = default;
        
        if (!_memoryCache.TryGetValue(request.AccountId, out List<TransactionEntity>? transactions))
        {
            transactions = new List<TransactionEntity>();
                
            _memoryCache.Set(request.AccountId, transactions, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(15)
            });
        }

        transactionId = new Random().Next(3000000, 3999999);

        var transaction = new TransactionEntity
        {
            Id = transactionId,
            AccountId = request.AccountId,
            Amount = request.Amount,
            Type = request.TransactionType
        };
            
        transactions?.Add(transaction);

        return await Task.FromResult(transactionId);
    }
}