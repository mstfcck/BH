namespace Web.BFF.API.Services.Models;

public class GetUserServiceResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public GetUserServiceResponse(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }
}