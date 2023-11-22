// using Account.Domain.DomainEvents;
// using MediatR;
//
// namespace Account.API.Application.DomainEventHandlers;
//
// public class AccountOpenedDomainEventHandler : INotificationHandler<AccountOpenedDomainEvent>
// {
//     private readonly ILogger<AccountOpenedDomainEventHandler> _logger;
//
//     public AccountOpenedDomainEventHandler(ILogger<AccountOpenedDomainEventHandler> logger)
//     {
//         _logger = logger;
//     }
//
//     public Task Handle(AccountOpenedDomainEvent notification, CancellationToken cancellationToken)
//     {
//         _logger.LogInformation("Domain Event {DomainEvent} ({Notification})", notification.GetType().Name, notification);
//
//         // Any action related to "Open Account".
//         return Task.CompletedTask;
//     }
// }