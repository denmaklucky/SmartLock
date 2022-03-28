using Domain.Queries.Locks;
using FluentValidation;

namespace Domain.Validators.Locks;

public class GetOpeningHistoryQueryValidator : AbstractValidator<GetOpeningHistoryQuery>
{
    public GetOpeningHistoryQueryValidator()
    {
        RuleFor(goh => goh.UserId).NotEqual(Guid.Empty);
    }
}