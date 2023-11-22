using MediatR;

namespace Account.Domain.DomainEvents;

public class AccountOpenedDomainEvent : INotification
{
    public Aggregates.AccountAggregate.Account Account { get; }

    public AccountOpenedDomainEvent(Aggregates.AccountAggregate.Account account)
    {
        Account = account;
    }
}