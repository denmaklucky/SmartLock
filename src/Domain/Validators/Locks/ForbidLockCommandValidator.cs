using Domain.Commands.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class ForbidLockCommandValidator : AbstractValidator<ForbidLockCommand>
{
    public ForbidLockCommandValidator()
    {
        RuleFor(fl => fl.ForbiddenBy).NotEqual(Guid.Empty);
        RuleFor(fl => fl.AccessId).Must(accessId => Guid.TryParse(accessId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(fl => fl.LockId).Must(lockId => Guid.TryParse(lockId, out _)).WithMessage("`{PropertyName}` should be GUID");
    }
}