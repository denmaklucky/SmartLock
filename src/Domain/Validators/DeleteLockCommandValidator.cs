using Domain.Commands;
using FluentValidation;

namespace Domain.Validators;

public class DeleteLockCommandValidator : AbstractValidator<DeleteLockCommand>
{
    public DeleteLockCommandValidator()
    {
        RuleFor(dl => dl.LockId).NotEqual(Guid.Empty);
        RuleFor(dl => dl.UserId).NotEqual(Guid.Empty);
    }
}