namespace Transaction.API.Models;

public class GetTransactionListResponse
{
    public IEnumerable<GetTransactionResponse> Transactions { get; set; }

    public GetTransactionListResponse()
    {
        Transactions = new List<GetTransactionResponse>();
    }
}