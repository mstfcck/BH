using MediatR;
using User.API.Models;

namespace User.API.Application.Queries;

public class GetUserListQuery : IRequest<GetUserListResponse>
{
}