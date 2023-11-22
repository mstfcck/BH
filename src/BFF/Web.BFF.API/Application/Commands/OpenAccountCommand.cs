using MediatR;
using Web.BFF.API.Models;

namespace Web.BFF.API.Application.Commands;

public class OpenAccountCommand : IRequest<int>
{
    public int CustomerId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType TransactionType { get; private set; }

    public OpenAccountCommand(int customerId, decimal amount, TransactionType transactionType)
    {
        CustomerId = customerId;
        Amount = amount;
        TransactionType = transactionType;
    }
}