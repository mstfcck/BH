using MediatR;
using User.API.Models;

namespace User.API.Application.Queries;

public class GetUserQuery : IRequest<GetUserResponse>
{
    /// <summary>
    /// UserId/CustomerId
    /// </summary>
    public int UserId { get; set; }

    public GetUserQuery(int userId)
    {
        UserId = userId;
    }
}