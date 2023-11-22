using Microsoft.Extensions.Caching.Memory;
using UserEntity = User.Domain.Aggregates.UserAggregate.User;

namespace User.API;

public static class InMemoryFakeDb
{
    public static void UseFakeUserData(this IServiceProvider services)
    {
        var memoryCache = services.GetService<IMemoryCache>();

        var initialUsers = new List<UserEntity>
        {
            new()
            {
                Id = 1000001, // User (Customer) Id
                Name = "Mustafa",
                Surname = "Çiçek"
            }
        };

        foreach (UserEntity user in initialUsers)
        {
            if (!memoryCache.TryGetValue(Constants.UsersCacheKey, out List<UserEntity>? users))
            {
                users = new List<UserEntity>();
                
                memoryCache.Set(Constants.UsersCacheKey, users, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(15)
                });
            }

            users?.Add(user);
        }
    }
}