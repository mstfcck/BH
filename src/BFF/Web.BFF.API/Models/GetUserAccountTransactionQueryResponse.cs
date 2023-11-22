namespace Web.BFF.API.Models;

public class GetUserAccountTransactionQueryResponse
{
    public GetUserQueryResponse User { get; set; }

    public GetUserAccountTransactionQueryResponse()
    {
        User = new GetUserQueryResponse();
    }
}

public class GetUserQueryResponse
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public List<GetAccountQueryResponse> Accounts { get; set; }

    public GetUserQueryResponse()
    {
        Accounts = new List<GetAccountQueryResponse>();
    }
}

public class GetAccountQueryResponse
{
    public int AccountId { get; set; }
    public List<GetTransactionQueryResponse> Transactions { get; set; }
}

public class GetTransactionQueryResponse    
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
}