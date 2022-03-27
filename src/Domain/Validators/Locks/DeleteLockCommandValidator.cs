using Domain.Commands.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class DeleteLockCommandValidator : AbstractValidator<DeleteLockCommand>
{
    public DeleteLockCommandValidator()
    {
        RuleFor(dl => dl.LockId).NotEmpty().Must(ol => Guid.TryParse(ol, out _));
        RuleFor(dl => dl.DeletedBy).NotEqual(Guid.Empty);
    }
}