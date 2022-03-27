using Domain.Commands;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results;
using FluentValidation;
using MediatR;
using Model;
using Model.Enums;
using Model.Models.Entities;

namespace Domain.Handlers;

public class CreateLockHandler : IRequestHandler<CreateLockCommand, CreateLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<CreateLockCommand> _validator;

    public CreateLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<CreateLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<CreateLockResult> Handle(CreateLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new CreateLockResult { ErrorCode = ErrorCodes.InvalidRequest, ValidatorErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");
        
        var newLock = new Lock
        {
            State = LockStateEnum.Online,
            Title = request.Title,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.UserId
        };

        var @lock = await _dataAccess.AddLock(newLock, cancellationToken);

        var newSetting = new LockSetting
        {
            LockId = @lock.Id,
            Mode = LockModeEnum.Standard,
            CreatedBy = request.UserId,
            CreatedOn = DateTime.UtcNow
        };

        var lockSetting = await _dataAccess.AddSetting(newSetting, cancellationToken);

        return new CreateLockResult
        {
            Data = new LockDto
            {
                Id = @lock.Id,
                State = @lock.State,
                Title = @lock.Title,
                Setting = new LockSettingDto
                {
                    Mode = lockSetting.Mode,
                    EndTime = lockSetting.EndTime,
                    StartTime = lockSetting.StartTime
                }
            }
        };
    }
}