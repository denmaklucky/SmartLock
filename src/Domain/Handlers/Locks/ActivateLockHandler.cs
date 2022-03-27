using Domain.Commands.Locks;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;
using Model.Enums;
using Model.Models.Entities;

namespace Domain.Handlers.Locks;

public class ActivateLockHandler : IRequestHandler<ActivateLockCommand, CreateLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<ActivateLockCommand> _validator;

    public ActivateLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<ActivateLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<CreateLockResult> Handle(ActivateLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new CreateLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var newLock = new Lock
        {
            ActivationKey = request.ActivationKey,
            Title = request.Title,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.UserId
        };

        var createdLock = await _dataAccess.AddLock(newLock, cancellationToken);

        var newSetting = new LockSetting
        {
            LockId = createdLock.Id,
            Mode = LockModeEnum.Standard,
            CreatedBy = request.UserId,
            CreatedOn = DateTime.UtcNow
        };

        var lockSetting = await _dataAccess.AddSetting(newSetting, cancellationToken);

        var accessLock = new AccessLock
        {
            LockId = createdLock.Id,
            AccessId = request.UserId,
            Type = AccessTypeEnum.User 
        };

        await _dataAccess.AddAccessLock(accessLock, cancellationToken);

        return new CreateLockResult
        {
            Data = new LockDto
            {
                Id = createdLock.Id,
                Title = createdLock.Title,
                IsDeleted = createdLock.IsDeleted,
                Setting = new LockSettingDto
                {
                    Mode = lockSetting.Mode,
                    EndOpenTime = lockSetting.EndOpenTime,
                    StartOpenTime = lockSetting.StartOpenTime
                }
            }
        };
    }
}