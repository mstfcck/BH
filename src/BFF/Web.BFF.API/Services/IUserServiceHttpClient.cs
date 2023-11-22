using Refit;
using Web.BFF.API.Services.Models;

namespace Web.BFF.API.Services;

public interface IUserServiceHttpClient
{
    [Get("/api/users/{id}")]
    Task<GetUserServiceResponse> GetUser(int id);
}