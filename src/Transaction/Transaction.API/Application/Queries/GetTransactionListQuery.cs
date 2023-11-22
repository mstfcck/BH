using MediatR;
using Transaction.API.Models;

namespace Transaction.API.Application.Queries;

public class GetTransactionListQuery : IRequest<GetTransactionListResponse>
{
    public int AccountId { get; set; }

    public GetTransactionListQuery(int accountId)
    {
        AccountId = accountId;
    }
}