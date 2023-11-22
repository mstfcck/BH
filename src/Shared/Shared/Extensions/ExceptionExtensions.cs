using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions;

public static class ExceptionExtensions
{
    public static IMvcBuilder AddModelStateValidationControl(this IMvcBuilder builder)
    {
        return builder.ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressMapClientErrors = true;
            options.InvalidModelStateResponseFactory = context => throw new ValidationException("context.ModelState");
        });
    }
}