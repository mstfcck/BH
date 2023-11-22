namespace User.API.Models;

public class GetUserListResponse
{
    public IEnumerable<GetUserResponse> Users { get; set; }
    
    public GetUserListResponse()
    {
        Users = new List<GetUserResponse>();
    }
}