using Domain.Queries.Keys;
using FluentValidation;

namespace Domain.Validators.Keys;

public class GetKeysQueryValidator : AbstractValidator<GetKeysQuery>
{
    public GetKeysQueryValidator()
    {
        RuleFor(gk => gk.CreatedBy).NotEqual(Guid.Empty);
    }
}