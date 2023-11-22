using Shared.Domain;
using Transaction.Domain.DomainEvents;

namespace Transaction.Domain.Aggregates.TransactionAggregate;

public class Transaction : Entity // IAggregateRoot
{
    public Transaction()
    {
        Date = DateTime.Now;
        
        AddDomainEvent(new TransactionCreatedDomainEvent(this));
    }
    
    /// <summary>
    /// TransactionId
    /// </summary>
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
    
    public DateTime Date { get; private set; }
}