namespace Transaction.API.Models;

public class GetTransactionResponse
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public int TransactionType { get; set; }

    public GetTransactionResponse(int transactionId, decimal amount, int transactionType)
    {
        TransactionId = transactionId;
        Amount = amount;
        TransactionType = transactionType;
    }
}