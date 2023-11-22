using MediatR;
using Microsoft.Extensions.Caching.Memory;
using UserEntity = User.Domain.Aggregates.UserAggregate.User;

namespace User.API.Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IMemoryCache _memoryCache;

    public CreateUserCommandHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!_memoryCache.TryGetValue(Constants.UsersCacheKey, out List<UserEntity>? users))
        {
            users = new List<UserEntity>();
                
            _memoryCache.Set(Constants.UsersCacheKey, users, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(15)
            });
        }

        var userId  = new Random().Next(1000000, 1999999);

        var user = new UserEntity
        {
            Id = userId,
            Name = request.Name,
            Surname = request.Surname
        };

        users?.Add(user);

        return await Task.FromResult(userId);
    }
}