using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using User.API.Application.Validators;

namespace User.API.UnitTests;

// The [TestFixture] attribute denotes a class that contains unit tests.
// The [Test] attribute indicates a method is a test method.

public class UserTests
{
    public WebApplicationBuilder Builder { get; set; }
    public WebApplication App { get; set; }
    public IMemoryCache? MemoryCache { get; set; }
    
    [SetUp]
    public void Setup()
    {
        Builder = WebApplication.CreateBuilder();
        Builder.Services.AddMemoryCache();
        Builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(InMemoryFakeDb).Assembly);

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
        Builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

        App = Builder.Build();
        App.Services.UseFakeUserData();
        
        MemoryCache = App.Services.GetService<IMemoryCache>();
    }
}