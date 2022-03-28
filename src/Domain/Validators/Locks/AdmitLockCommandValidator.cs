using Domain.Commands.Locks;
using FluentValidation;
using Model.Enums;

namespace Domain.Validators.Locks;

public class AdmitLockCommandValidator : AbstractValidator<AdmitLockCommand>
{
    public AdmitLockCommandValidator()
    {
        RuleFor(al => al.AdmittedBy).NotEqual(Guid.Empty);
        RuleFor(al => al.AccessId).NotEmpty().Must(ol => Guid.TryParse(ol, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(al => al.Type).Must(type => Enum.IsDefined(typeof(AccessTypeEnum), type));
    }
}