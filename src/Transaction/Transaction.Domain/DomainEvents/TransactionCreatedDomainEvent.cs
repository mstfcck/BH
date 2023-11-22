using MediatR;

namespace Transaction.Domain.DomainEvents;

public class TransactionCreatedDomainEvent : INotification
{
    public Aggregates.TransactionAggregate.Transaction Transaction { get; }

    public TransactionCreatedDomainEvent(Aggregates.TransactionAggregate.Transaction transaction)
    {
        Transaction = transaction;
    }
}