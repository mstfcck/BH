namespace Account.API.Models;

public class OpenAccountResponse
{
    public int AccountId { get; set; }

    public OpenAccountResponse(int accountId)
    {
        AccountId = accountId;
    }
}