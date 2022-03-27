using Domain.Commands.Locks;
using FluentValidation;
using Model.Enums;

namespace Domain.Validators.Locks;

public class UpdateLockCommandValidator : AbstractValidator<UpdateLockCommand>
{
    public UpdateLockCommandValidator()
    {
        RuleFor(ul => ul.UserId).NotEqual(Guid.Empty);
        RuleFor(ul => ul.LockId).Must(lockId => Guid.TryParse(lockId, out _));
        When(ul => ul.Mode != null, () => { RuleFor(ul => ul.Mode).Must(providedMode => Enum.IsDefined(typeof(LockModeEnum), providedMode)); });
        When(ul => ul.EndOpenTime != null, () => { RuleFor(ul => ul.EndOpenTime).Must(endTime => endTime.GetValueOrDefault() != default); });
        When(ul => ul.StartOpenTime != null, () => { RuleFor(ul => ul.StartOpenTime).Must(startTime => startTime.GetValueOrDefault() != default); });
    }
}