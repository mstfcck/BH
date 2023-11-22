using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandName = request.GetType().Name;

        // NOTE: We can handle the database transactions here such as begin and commit transactions.
        
        _logger.LogInformation("Command transaction {CommandName} handling - request: ({@Request})", commandName, request);
        
        var response = await next();
        
        _logger.LogInformation("Command transaction {CommandName} handled - response: {@Response}", commandName, response);

        return response;
    }
}