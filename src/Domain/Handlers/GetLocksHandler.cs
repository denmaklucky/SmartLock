using Domain.Queries;
using Domain.Results;
using MediatR;
using Model;

namespace Domain.Handlers;

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