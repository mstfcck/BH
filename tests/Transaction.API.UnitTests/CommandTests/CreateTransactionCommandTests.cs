using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Transaction.API.Application.Commands;
using Transaction.Domain.Aggregates.TransactionAggregate;

namespace Transaction.API.UnitTests.CommandTests;

[TestFixture]
public class CreateTransactionCommandTests : TransactionTests
{
    [TestCase(2000001, 1000, TransactionType.Deposit)]
    public void Handle_NullMemoryCache_ThrowsException(int accountId, decimal amount, TransactionType transactionType)
    {
        // Arrange
        IMemoryCache memoryCache = null;
        
        var handler = new CreateTransactionCommandHandler(memoryCache);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(() =>
            handler.Handle(new CreateTransactionCommand(accountId, amount, transactionType), CancellationToken.None));
    }
    
    [TestCase(2000001, 1000, TransactionType.Deposit)]
    [TestCase(2000001, 2000, TransactionType.Withdrawal)]
    public async Task Handle_ExistingCustomerId_ReturnsGeneratedTransactionId(int accountId, decimal amount, TransactionType transactionType)
    {
        var handler = new CreateTransactionCommandHandler(MemoryCache);
    
        // Act
        int transactionId = await handler.Handle(new CreateTransactionCommand(accountId, amount, transactionType), CancellationToken.None);
    
        // Assert
        transactionId.Should().NotBe(null);
        transactionId.Should().BeInRange(3000000, 3999999);
    }
    
    [TestCase(2000002, 1000, TransactionType.Deposit)]
    [TestCase(2000002, 2000, TransactionType.Withdrawal)]
    public async Task Handle_NonExistingCustomerId_ReturnsGeneratedTransactionId(int accountId, decimal amount, TransactionType transactionType)
    {
        var handler = new CreateTransactionCommandHandler(MemoryCache);
    
        // Act
        int transactionId = await handler.Handle(new CreateTransactionCommand(accountId, amount, transactionType), CancellationToken.None);
    
        // Assert
        transactionId.Should().NotBe(null);
        transactionId.Should().BeInRange(3000000, 3999999);
    }
}