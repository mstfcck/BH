using Account.API.Application.Commands;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace Account.API.UnitTests.CommandTests;

[TestFixture]
public class OpenAccountCommandTests : AccountTests
{
    [TestCase(1000001)]
    public void Handle_NullMemoryCache_ThrowsException(int customerId)
    {
        // Arrange
        IMemoryCache memoryCache = null;
        
        var handler = new OpenAccountCommandHandler(memoryCache);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(() =>
            handler.Handle(new OpenAccountCommand(customerId), CancellationToken.None));
    }
    
    [TestCase(1000001)]
    public async Task Handle_ExistingCustomerId_ReturnsGeneratedAccountId(int customerId)
    {
        var handler = new OpenAccountCommandHandler(MemoryCache);
    
        // Act
        int accountId = await handler.Handle(new OpenAccountCommand(customerId), CancellationToken.None);
    
        // Assert
        accountId.Should().NotBe(null);
        accountId.Should().BeInRange(2000000, 2999999);
    }
    
    [TestCase(1000002)]
    public async Task Handle_NonExistingCustomerId_ReturnsGeneratedAccountId(int customerId)
    {
        var handler = new OpenAccountCommandHandler(MemoryCache);
    
        // Act
        int accountId = await handler.Handle(new OpenAccountCommand(customerId), CancellationToken.None);
    
        // Assert
        accountId.Should().NotBe(null);
        accountId.Should().BeInRange(2000000, 2999999);
    }
}