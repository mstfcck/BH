namespace Web.BFF.API.Services.Models;

public class GetAccountListServiceResponse
{
    public GetAccountListServiceResponse()
    {
        Accounts = new List<GetAccountServiceResponse>();
    }
    
    public IEnumerable<GetAccountServiceResponse> Accounts { get; set; }
}

public class GetAccountServiceResponse
{
    public int AccountId { get; set; }
}