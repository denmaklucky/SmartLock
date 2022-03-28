using Domain.Commands.Keys;
using FluentValidation;

namespace Domain.Validators.Keys;

public class ChangeLockForKeyCommandValidator : AbstractValidator<ChangeLockForKeyCommand>
{
    public ChangeLockForKeyCommandValidator()
    {
        RuleFor(cl => cl.UpdatedBy).NotEqual(Guid.Empty);
        RuleFor(cl => cl.NewLockId).NotEmpty().Must(newLockId => Guid.TryParse(newLockId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(cl => cl.OldLockId).NotEmpty().Must(oldLockId => Guid.TryParse(oldLockId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ck => ck.KeyId).NotEmpty().Must(keyId => Guid.TryParse(keyId, out _)).WithMessage("`{PropertyName}` should be GUID");
    }
}