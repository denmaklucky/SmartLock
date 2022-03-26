using Domain.Dto;
using Domain.Queries;
using MediatR;
using Model;

namespace Domain.Handlers;

public class GetLocksHandler : IRequestHandler<GetLocksQuery, List<LockDto>>
{
    private readonly IDataAccess _dataAccess;

    public GetLocksHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public Task<List<LockDto>> Handle(GetLocksQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<LockDto> { new LockDto { Name = "SmartLocker" } });
    }
}