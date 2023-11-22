using Account.API.Models;
using MediatR;

namespace Account.API.Application.Queries;

public class GetAccountListQuery : IRequest<GetAccountListResponse>
{
    public int CustomerId { get; set; }

    public GetAccountListQuery(int customerId)
    {
        CustomerId = customerId;
    }
}