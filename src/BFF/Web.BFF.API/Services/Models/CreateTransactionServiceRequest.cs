namespace Web.BFF.API.Services.Models;

public class CreateTransactionServiceRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public int TransactionType { get; set; }
}