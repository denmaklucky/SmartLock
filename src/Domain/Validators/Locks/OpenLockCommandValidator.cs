using Domain.Commands.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class OpenLockCommandValidator : AbstractValidator<OpenLockCommand>
{
    public OpenLockCommandValidator()
    {
        When(ol => !string.IsNullOrEmpty(ol.KeyId), () =>  RuleFor(ol => ol.KeyId).NotEmpty().Must(ol => Guid.TryParse(ol, out _)).WithMessage("`{PropertyName}` should be GUID"));
        RuleFor(ol => ol.LockId).NotEmpty().Must(ol => Guid.TryParse(ol, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ol => ol.OpenedBy).NotEqual(Guid.Empty);
    }
}