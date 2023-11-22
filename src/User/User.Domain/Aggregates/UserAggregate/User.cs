using Shared.Domain;

namespace User.Domain.Aggregates.UserAggregate;

public class User : Entity // IAggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public User()
    {
    }
}