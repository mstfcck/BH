namespace Web.BFF.API.Services.Models;

public class OpenAccountServiceResponse
{
    public int AccountId { get; set; }

    public OpenAccountServiceResponse(int accountId)
    {
        AccountId = accountId;
    }
}