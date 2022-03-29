using Domain.Commands.Keys;
using FluentValidation;

namespace Domain.Validators.Keys;

public class DeleteKeyCommandValidator : AbstractValidator<DeleteKeyCommand>
{
    public DeleteKeyCommandValidator()
    {
        RuleFor(dc => dc.DeletedBy).NotEqual(Guid.Empty);
        RuleFor(dc => dc.KeyId).NotEmpty().Must(keyId => Guid.TryParse(keyId, out _)).WithMessage("`{PropertyName}` should be GUID");
    }
}