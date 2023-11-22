using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using User.API.Application.Commands;

namespace User.API.UnitTests.CommandTests;

[TestFixture]
public class CreateUserCommandTests : UserTests
{
    [TestCase("Mustafa", "Çiçek")]
    public void Handle_NullMemoryCache_ThrowsException(string name, string surname)
    {
        // Arrange
        IMemoryCache memoryCache = null;
        
        var handler = new CreateUserCommandHandler(memoryCache);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(() =>
            handler.Handle(new CreateUserCommand(name, surname), CancellationToken.None));
    }
    
    [TestCase("Mustafa", "Çiçek")]
    public async Task Handle_ExistingCustomerId_ReturnsGeneratedTransactionId(string name, string surname)
    {
        var handler = new CreateUserCommandHandler(MemoryCache);
    
        // Act
        int transactionId = await handler.Handle(new CreateUserCommand(name, surname), CancellationToken.None);
    
        // Assert
        transactionId.Should().NotBe(null);
        transactionId.Should().BeInRange(1000000, 1999999);
    }
}