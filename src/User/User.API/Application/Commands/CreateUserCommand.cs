using MediatR;

namespace User.API.Application.Commands;

public class CreateUserCommand : IRequest<int>
{
    public string Name { get; private set; }
    public string Surname { get; private set; }

    public CreateUserCommand(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}