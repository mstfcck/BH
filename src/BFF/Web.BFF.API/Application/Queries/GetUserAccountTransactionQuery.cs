using MediatR;
using Web.BFF.API.Models;

namespace Web.BFF.API.Application.Queries;

public class GetUserAccountTransactionQuery : IRequest<GetUserAccountTransactionQueryResponse>
{
    public int CustomerId { get; set; }

    public GetUserAccountTransactionQuery(int customerId)
    {
        CustomerId = customerId;
    }
}