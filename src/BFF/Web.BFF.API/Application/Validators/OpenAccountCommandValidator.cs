using FluentValidation;
using Web.BFF.API.Application.Commands;

namespace Web.BFF.API.Application.Validators;

public class OpenAccountCommandValidator : AbstractValidator<OpenAccountCommand>
{
    public OpenAccountCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TransactionType)
            .IsInEnum();
    }
}