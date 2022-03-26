using Domain.Commands;
using FluentValidation;

namespace Domain.Validators;

public class CreateLockCommandValidator : AbstractValidator<CreateLockCommand>
{
    public CreateLockCommandValidator()
    {
        RuleFor(c => c.ActivationKey).NotEmpty();
        RuleFor(c => c.UserId).NotEqual(Guid.Empty);
    }
}