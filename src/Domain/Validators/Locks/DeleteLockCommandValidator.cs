using Domain.Commands.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class DeleteLockCommandValidator : AbstractValidator<DeleteLockCommand>
{
    public DeleteLockCommandValidator()
    {
        RuleFor(dl => dl.LockId).NotEqual(Guid.Empty);
        RuleFor(dl => dl.UserId).NotEqual(Guid.Empty);
    }
}