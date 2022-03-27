using Domain.Queries.Locks;
using Domain.Results.Locks;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class GetLocksHandler : IRequestHandler<GetLocksQuery, GetLocksResult>
{
    private readonly IDataAccess _dataAccess;

    public GetLocksHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public Task<GetLocksResult> Handle(GetLocksQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetLocksResult());
    }
}