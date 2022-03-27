using Domain.Queries.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class GetLocksQueryValidator : AbstractValidator<GetLocksQuery>
{
    public GetLocksQueryValidator()
    {
        RuleFor(gl => gl.UserId).NotEqual(Guid.Empty);
    }
}