namespace Account.API.Models;

public class GetAccountListResponse
{
    public IEnumerable<GetAccountResponse> Accounts { get; set; }

    public GetAccountListResponse()
    {
        Accounts = new List<GetAccountResponse>();
    }
}