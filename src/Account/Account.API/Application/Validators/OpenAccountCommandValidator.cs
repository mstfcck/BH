using Account.API.Application.Commands;
using FluentValidation;

namespace Account.API.Application.Validators;

public class OpenAccountCommandValidator : AbstractValidator<OpenAccountCommand>
{
    public OpenAccountCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}