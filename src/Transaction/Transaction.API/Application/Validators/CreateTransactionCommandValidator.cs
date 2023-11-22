using FluentValidation;
using Transaction.API.Application.Commands;

namespace Transaction.API.Application.Validators;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TransactionType)
            .IsInEnum();
    }
}