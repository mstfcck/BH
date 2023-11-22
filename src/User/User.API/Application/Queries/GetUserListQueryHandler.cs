using MediatR;
using Microsoft.Extensions.Caching.Memory;
using User.API.Models;
using UserEntity = User.Domain.Aggregates.UserAggregate.User;

namespace User.API.Application.Queries;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, GetUserListResponse>
{
    private readonly IMemoryCache _memoryCache;

    public GetUserListQueryHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<GetUserListResponse> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        _memoryCache.TryGetValue(Constants.UsersCacheKey, out List<UserEntity>? users);

        var response = new GetUserListResponse();
        
        if (users != null)
        {
            response.Users = users.Select(x => new GetUserResponse(x.Id, x.Name, x.Surname));
        }

        return await Task.FromResult(response);
    }
}