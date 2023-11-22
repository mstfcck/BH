using Account.Domain.DomainEvents;
using Shared.Domain;

namespace Account.Domain.Aggregates.AccountAggregate;

public class Account : Entity // IAggregateRoot
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public Account()
    {
        AddDomainEvent(new AccountOpenedDomainEvent(this));
    }
}