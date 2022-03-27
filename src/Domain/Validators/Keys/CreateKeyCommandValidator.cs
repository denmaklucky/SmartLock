using Domain.Commands.Keys;
using FluentValidation;
using Model.Enums;

namespace Domain.Validators.Keys;

public class CreateKeyCommandValidator : AbstractValidator<CreateKeyCommand>
{
    public CreateKeyCommandValidator()
    {
        RuleFor(ck => ck.UserId).NotEqual(Guid.Empty);
        RuleFor(ck => ck.LockId).Must(lockId => Guid.TryParse(lockId, out _)).WithMessage("`{PropertyName}` should be GUID");
        RuleFor(ck => ck.Type).Must(type => Enum.IsDefined(typeof(KeyTypeEnum), type));
        When(ck => ck.Type == KeyTypeEnum.eKey, () =>
        {
            RuleFor(ck => ck.ExpiredAt).NotNull();
            RuleFor(ck => ck.ExpiredAt).GreaterThan(DateTime.UtcNow);
        });
    }
}