using Domain.Queries.Locks;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class GetLocksHandler : IRequestHandler<GetLocksQuery, GetLocksResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<GetLocksQuery> _validator;

    public GetLocksHandler(IDataAccess dataAccess, IMediator mediator, IValidator<GetLocksQuery> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public Task<GetLocksResult> Handle(GetLocksQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetLocksResult());
    }
}