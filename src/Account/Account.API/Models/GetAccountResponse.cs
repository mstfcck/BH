namespace Account.API.Models;

public class GetAccountResponse
{
    public int AccountId { get; set; }

    public GetAccountResponse(int accountId)
    {
        AccountId = accountId;
    }
}