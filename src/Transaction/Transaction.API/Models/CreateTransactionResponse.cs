namespace Transaction.API.Models;

public class CreateTransactionResponse
{
    public int TransactionId { get; set; }

    public CreateTransactionResponse(int transactionId)
    {
        TransactionId = transactionId;
    }
}