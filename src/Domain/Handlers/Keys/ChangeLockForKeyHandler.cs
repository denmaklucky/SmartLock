using Domain.Commands.Keys;
using Domain.Commands.Locks;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Keys;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Keys;

public class ChangeLockForKeyHandler : IRequestHandler<ChangeLockForKeyCommand, ChangeLockForKeyResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<ChangeLockForKeyCommand> _validator;

    public ChangeLockForKeyHandler(IDataAccess dataAccess, IMediator mediator, IValidator<ChangeLockForKeyCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<ChangeLockForKeyResult> Handle(ChangeLockForKeyCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new ChangeLockForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUpdatedByResult = await _mediator.Send(new GetUserQuery(request.UpdatedBy), cancellationToken);

        if (!getUpdatedByResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UpdatedBy}");

        var keyId = Guid.Parse(request.KeyId);
        var checkKeyResult = await _mediator.Send(new CheckKeyCommand(keyId), cancellationToken);

        if (!checkKeyResult.IsSuccess)
            return new ChangeLockForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { $"Key with id {keyId} is not valid. Please try different one." } };

        var oldLockId = Guid.Parse(request.OldLockId);
        var checkOldLockResult = await _mediator.Send(new CheckLockCommand(oldLockId), cancellationToken);
        
        if (!checkOldLockResult.IsSuccess)
            return new ChangeLockForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { $"Lock with id `{oldLockId}` not found."} };
            
        var canOpenLockByKeyResult = await _mediator.Send(new CanOpenLockByKeyCommand(keyId, oldLockId), cancellationToken);
        
        if (!canOpenLockByKeyResult.IsSuccess)
            return new ChangeLockForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{$"Key with id {keyId} is not valid. Please try different one."}};

        var newLockId = Guid.Parse(request.NewLockId);
        var checkNewLockResult = await _mediator.Send(new CheckLockCommand(newLockId), cancellationToken);
        
        if (!checkNewLockResult.IsSuccess)
            return new ChangeLockForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { $"Lock with id `{oldLockId}` not found."} };
        
        var accessLock = await _dataAccess.GetAccessLock(keyId, oldLockId, cancellationToken);

        accessLock.LockId = newLockId;
        accessLock.ModifiedBy = request.UpdatedBy;
        accessLock.ModifiedOn = DateTime.UtcNow;

        var updatedAccessLock = await _dataAccess.UpdateAccessLock(accessLock, cancellationToken);

        return new ChangeLockForKeyResult
        {
            Data = new AccessLockDto
            {
                Id = updatedAccessLock.Id,
                Type = updatedAccessLock.Type,
                AccessId = updatedAccessLock.AccessId,
                IsDeleted = updatedAccessLock.IsDeleted,
                LockId = updatedAccessLock.LockId
            }
        };
    }
}