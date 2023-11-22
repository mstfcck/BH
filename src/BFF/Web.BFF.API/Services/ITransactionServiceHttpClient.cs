using Refit;
using Web.BFF.API.Services.Models;

namespace Web.BFF.API.Services;

public interface ITransactionServiceHttpClient
{
    [Post("/api/transactions")]
    Task<CreateTransactionServiceResponse> CreateTransaction([Body] CreateTransactionServiceRequest request);

    [Get("/api/transactions/account/{accountId}")]
    Task<GetTransactionListServiceResponse> GetAccountTransactionList(int accountId);
}