using Domain.Commands;
using FluentValidation;

namespace Domain.Validators;

public class CreateLockCommandValidator : AbstractValidator<CreateLockCommand>
{
    public CreateLockCommandValidator()
    {
        RuleFor(c => c.ActivationKey).NotEmpty().Equal(Constants.RightActivationKey).WithMessage("Activation key is not valid, try different on.");
        RuleFor(c => c.UserId).NotEqual(Guid.Empty);
    }
}