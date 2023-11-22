namespace Web.BFF.API.Services.Models;

public class GetTransactionListServiceResponse
{
    public IEnumerable<GetTransactionServiceResponse> Transactions { get; set; }

    public GetTransactionListServiceResponse()
    {
        Transactions = new List<GetTransactionServiceResponse>();
    }
}

public class GetTransactionServiceResponse
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public int TransactionType { get; set; }

    public GetTransactionServiceResponse(int transactionId, decimal amount, int transactionType)
    {
        TransactionId = transactionId;
        Amount = amount;
        TransactionType = transactionType;
    }
}