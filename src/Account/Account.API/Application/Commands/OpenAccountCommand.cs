using MediatR;

namespace Account.API.Application.Commands;

public class OpenAccountCommand : IRequest<int>
{
    public int CustomerId { get; private set; }

    public OpenAccountCommand(int customerId)
    {
        CustomerId = customerId;
    }
}