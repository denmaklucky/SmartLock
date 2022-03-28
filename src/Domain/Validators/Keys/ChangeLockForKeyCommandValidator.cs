using Domain.Commands.Keys;
using FluentValidation;

namespace Domain.Validators.Keys;

public class ChangeLockForKeyCommandValidator : AbstractValidator<ChangeLockForKeyCommand>
{
    public ChangeLockForKeyCommandValidator()
    {
        RuleFor(uk => uk.UpdatedBy).NotEqual(Guid.Empty);
        RuleFor(ck => ck.NewLockId).NotEmpty().Must(lockId => Guid.TryParse(lockId, out _)).WithMessage("`{PropertyName}` should be GUID");
    }
}