using Domain.Commands.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class ActivateLockCommandValidator : AbstractValidator<ActivateLockCommand>
{
    public ActivateLockCommandValidator()
    {
        RuleFor(c => c.ActivationKey).NotEmpty().Equal(Constants.RightActivationKey).WithMessage("Activation key is not valid, try different on.");
        RuleFor(c => c.ActivatedBy).NotEqual(Guid.Empty);
    }
}