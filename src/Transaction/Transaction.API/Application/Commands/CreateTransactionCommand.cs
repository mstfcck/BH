using MediatR;
using Transaction.Domain.Aggregates.TransactionAggregate;

namespace Transaction.API.Application.Commands;

public class CreateTransactionCommand : IRequest<int>
{
    public int AccountId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType TransactionType { get; private set; }

    public CreateTransactionCommand(int accountId, decimal amount, TransactionType transactionType)
    {
        AccountId = accountId;
        Amount = amount;
        TransactionType = transactionType;
    }
}