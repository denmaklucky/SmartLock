using Domain.Commands.Locks;
using Domain.Results.Locks;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class CheckLockHandler : IRequestHandler<CheckLockCommand, CheckLockResult>
{
    private readonly IDataAccess _dataAccess;

    public CheckLockHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public async Task<CheckLockResult> Handle(CheckLockCommand request, CancellationToken cancellationToken)
    {
        var @lock = await _dataAccess.GetLock(request.LockId, cancellationToken);
        return @lock == null || @lock.IsDeleted
            ? new CheckLockResult { ErrorCode = ErrorCodes.NotFound }
            : new CheckLockResult();
    }
}