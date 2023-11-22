using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Refit;
using Shared.Behaviors;
using Shared.Extensions;
using Shared.Middlewares;
using Web.BFF.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

#region Refit Settings

var serviceSettings = builder.Configuration.GetSection("ServiceSettings").Get<ServiceSettings>();

builder.Services
    .AddRefitClient<IAccountServiceHttpClient>()
    .ConfigureHttpMessageHandlerBuilder(x => x.PrimaryHandler = new RefitHttpClientHandler())
    .ConfigureHttpClient(x => { x.BaseAddress = new Uri(serviceSettings.Account); });

builder.Services
    .AddRefitClient<ITransactionServiceHttpClient>()
    .ConfigureHttpMessageHandlerBuilder(x => x.PrimaryHandler = new RefitHttpClientHandler())
    .ConfigureHttpClient(x => { x.BaseAddress = new Uri(serviceSettings.Transaction); });

builder.Services
    .AddRefitClient<IUserServiceHttpClient>()
    .ConfigureHttpMessageHandlerBuilder(x => x.PrimaryHandler = new RefitHttpClientHandler())
    .ConfigureHttpClient(x => { x.BaseAddress = new Uri(serviceSettings.User); });

#endregion

builder.ConfigureLogging();

// MediatR Configurations 
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation Configurations
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();