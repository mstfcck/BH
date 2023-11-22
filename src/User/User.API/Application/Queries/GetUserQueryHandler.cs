using MediatR;
using Microsoft.Extensions.Caching.Memory;
using User.API.Models;
using UserEntity = User.Domain.Aggregates.UserAggregate.User;

namespace User.API.Application.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IMemoryCache _memoryCache;

    public GetUserQueryHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _memoryCache.TryGetValue(Constants.UsersCacheKey, out List<UserEntity>? users);

        var user = users!.Find(x => x.Id == request.UserId);

        if (user != null)
        {
            return await Task.FromResult(new GetUserResponse(user.Id, user.Name, user.Surname));
        }

        return await Task.FromResult<GetUserResponse>(null);
    }
}