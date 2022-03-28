using Domain.Commands.Keys;
using FluentValidation;

namespace Domain.Validators.Keys;

public class ChangeUserForKeyCommandValidator : AbstractValidator<ChangeUserForKeyCommand>
{
    public ChangeUserForKeyCommandValidator()
    {
        RuleFor(uk => uk.UpdatedBy).NotEqual(Guid.Empty);
        RuleFor(ck => ck.NewUserId).NotEmpty().Must(userId => Guid.TryParse(userId, out _)).WithMessage("`{PropertyName}` should be GUID");
    }
}