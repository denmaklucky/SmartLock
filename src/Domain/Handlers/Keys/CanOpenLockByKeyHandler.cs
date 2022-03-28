using Domain.Commands.Keys;
using Domain.Results.Keys;
using MediatR;
using Model;

namespace Domain.Handlers.Keys;

public class CanOpenLockByKeyHandler : IRequestHandler<CanOpenLockByKeyCommand, CanOpenLockByKeyResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;

    public CanOpenLockByKeyHandler(IDataAccess dataAccess, IMediator mediator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
    }
    
    public async Task<CanOpenLockByKeyResult> Handle(CanOpenLockByKeyCommand request, CancellationToken cancellationToken)
    {
        var checkKeyResult = await _mediator.Send(new CheckKeyCommand(request.KeyId), cancellationToken);

        if (!checkKeyResult.IsSuccess)
            return new CanOpenLockByKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{$"Key with id {request.KeyId} is not valid. Please try different one."}};
        
        var keyLock = await _dataAccess.GetAccessLock(request.KeyId, request.LockId, cancellationToken);

        if (keyLock == null)
            return new CanOpenLockByKeyResult { ErrorCode = ErrorCodes.NotFound, Messages = new[] { $"Couldn't key with id `{request.KeyId}` for lock with id `{request.LockId}`" } };

        return new CanOpenLockByKeyResult();
    }
}