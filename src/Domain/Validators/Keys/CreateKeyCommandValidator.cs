using Domain.Commands.Keys;
using FluentValidation;
using Model.Enums;

namespace Domain.Validators.Keys;

public class CreateKeyCommandValidator : AbstractValidator<CreateKeyCommand>
{
    public CreateKeyCommandValidator()
    {
        RuleFor(ck => ck.CreatedBy).NotEqual(Guid.Empty);
        RuleFor(ck => ck.UserId).Must(userId => Guid.TryParse(userId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ck => ck.LockId).Must(lockId => Guid.TryParse(lockId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ck => ck.Type).Must(type => Enum.IsDefined(typeof(KeyTypeEnum), type));
    }
}