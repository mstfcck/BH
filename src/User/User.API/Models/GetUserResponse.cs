namespace User.API.Models;

public class GetUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public GetUserResponse(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }
}