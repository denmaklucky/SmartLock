using Domain.Commands.Locks;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class UpdateLockHandler : IRequestHandler<UpdateLockCommand, UpdateLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<UpdateLockCommand> _validator;

    public UpdateLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<UpdateLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public async Task<UpdateLockResult> Handle(UpdateLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new UpdateLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var @lock = await _dataAccess.GetLock(Guid.Parse(request.LockId), cancellationToken);

        if (@lock == null)
            return new UpdateLockResult { ErrorCode = ErrorCodes.NotFound, Messages = new[] { $"Couldn't find a lock by following `lockId` {request.LockId}" } };

        @lock.Title = string.IsNullOrWhiteSpace(request.Title) ? @lock.Title : request.Title;
        @lock.ModifiedBy = request.UserId;
        @lock.ModifiedOn = DateTime.UtcNow;

        var lockSetting = @lock.Setting;
        lockSetting.Mode = request.Mode ?? lockSetting.Mode;
        lockSetting.StartOpenTime = request.StartOpenTime ?? lockSetting.StartOpenTime;
        lockSetting.EndOpenTime = request.EndOpenTime ?? lockSetting.EndOpenTime;

        var updatedLock = await _dataAccess.UpdateLock(@lock, cancellationToken);

        return new UpdateLockResult
        {
            Data = new LockDto
            {
                Id = updatedLock.Id,
                State = updatedLock.State,
                Title = updatedLock.Title,
                Setting = new LockSettingDto
                {
                    Mode = updatedLock.Setting.Mode,
                    EndOpenTime = updatedLock.Setting.EndOpenTime,
                    StartOpenTime = updatedLock.Setting.StartOpenTime
                }
            }
        };
    }
}