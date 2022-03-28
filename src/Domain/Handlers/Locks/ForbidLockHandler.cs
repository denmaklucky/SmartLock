using Domain.Commands.Locks;
using Domain.Dto;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class ForbidLockHandler : IRequestHandler<ForbidLockCommand, ForbidLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<ForbidLockCommand> _validator;

    public ForbidLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<ForbidLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public async Task<ForbidLockResult> Handle(ForbidLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new ForbidLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };
        
        var lockId = Guid.Parse(request.LockId);
        var checkLockResult = await _mediator.Send(new CheckLockCommand(lockId), cancellationToken);

        if (!checkLockResult.IsSuccess)
            return new ForbidLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { $"Lock with id `{lockId}` not found."} };

        var accessId = Guid.Parse(request.AccessId);
        var accessLock = await _dataAccess.GetAccessLock(accessId, lockId, cancellationToken);

        if (accessLock == null || accessLock.IsDeleted)
            return new ForbidLockResult { ErrorCode = ErrorCodes.NotFound, Messages = new[] { $"Couldn't access with id `{request.AccessId}` for lock with id `{request.LockId}`" } };

        accessLock.IsDeleted = true;
        accessLock.ModifiedBy = request.ForbiddenBy;
        accessLock.ModifiedOn = DateTime.UtcNow;

        var updatedAccessLock = await _dataAccess.UpdateAccessLock(accessLock, cancellationToken);
        return new ForbidLockResult
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