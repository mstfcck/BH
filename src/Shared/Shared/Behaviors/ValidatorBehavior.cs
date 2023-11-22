using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandName = request.GetType().Name;

        _logger.LogInformation("Validation command {CommandName} handling - request: ({@Request})", commandName, request);

        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger.LogWarning("Validation command {CommandName} - request: {@Request} - errors: {@ValidationErrors}", commandName, request, failures);
            throw new ValidationException($"Validation command errors for type {commandName}", failures);
        }

        var response = await next();
        
        _logger.LogInformation("Validation command {CommandName} handled - response: {@Response}", request.GetType().Name, response);

        return response;
    }
}