using Domain.Commands.Keys;
using FluentValidation;

namespace Domain.Validators.Keys;

public class AssignKeyToUserValidator : AbstractValidator<AssignKeyToUserCommand>
{
    public AssignKeyToUserValidator()
    {
        RuleFor(ak => ak.UserId).NotEqual(Guid.Empty);
        RuleFor(ak => ak.KeyId).Must(keyId => Guid.TryParse(keyId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ak => ak.AssignTo).Must(assignTo => Guid.TryParse(assignTo, out _)).WithMessage("`{PropertyName}` should be GUID");
    }
}