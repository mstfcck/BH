using Refit;
using Web.BFF.API.Services.Models;

namespace Web.BFF.API.Services;

public interface IAccountServiceHttpClient
{
    [Post("/api/accounts")]
    Task<OpenAccountServiceResponse> OpenAccount([Body] OpenAccountServiceRequest request);
    
    [Get("/api/accounts/customer/{customerId}")]
    Task<GetAccountListServiceResponse> GetCustomerAccountList(int customerId);
}