using Domain.Commands.Locks;
using FluentValidation;
using Model.Enums;

namespace Domain.Validators.Locks;

public class OpenLockCommandValidator : AbstractValidator<OpenLockCommand>
{
    public OpenLockCommandValidator()
    {
        When(ol => ol.AccessType == AccessTypeEnum.Key, () =>  RuleFor(ol => ol.KeyId).NotEmpty().Must(ol => Guid.TryParse(ol, out _)).WithMessage("`{PropertyName}` should be GUID"));
        RuleFor(ol => ol.LockId).NotEmpty().Must(ol => Guid.TryParse(ol, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ol => ol.OpenedBy).NotEqual(Guid.Empty);
    }
}