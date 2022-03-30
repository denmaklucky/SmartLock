using Domain.Commands.Keys;
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

public class OpenLockHandler : IRequestHandler<OpenLockCommand, OpenLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<OpenLockCommand> _validator;

    public OpenLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<OpenLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public async Task<OpenLockResult> Handle(OpenLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new OpenLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.OpenedBy), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.OpenedBy}");

        var @lock = await _dataAccess.GetLock(Guid.Parse(request.LockId), cancellationToken);

        if (@lock == null)
            return new OpenLockResult { ErrorCode = ErrorCodes.NotFound, Messages = new[] { $"Couldn't find a lock by following `lockId` {request.LockId}" } };

        if (@lock.IsDeleted)
            throw new LogicException(ErrorCodes.InternalError, $"The lock {@lock.Id} is deleted");

        var openHistory = new OpeningHistory
        {
            LockId = @lock.Id,
            UserName = getUserResult.UserName,
            CreatedBy = request.OpenedBy,
            CreatedOn = DateTime.UtcNow,
            AccessType = request.AccessType
        };

        if (request.AccessType == AccessTypeEnum.Key)
        {
            var keyId = Guid.Parse(request.KeyId);
            var canOpenLockByKeyResult = await _mediator.Send(new CanOpenLockByKeyCommand(keyId, @lock.Id), cancellationToken);

            if (!canOpenLockByKeyResult.IsSuccess)
                return new OpenLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{"You can't open the lock by following key. Please try different one."}};

            openHistory.AccessId = keyId;
        }
        else
        {
            var accessLock = await _dataAccess.GetAccessLock(request.OpenedBy, @lock.Id, cancellationToken);

            if (accessLock == null || accessLock.IsDeleted)
                return new OpenLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { "User doesn't have access to open the lock" } };

            openHistory.AccessId = request.OpenedBy;
        }

        @lock.OpeningHistories.Add(openHistory);

        var _ = await _dataAccess.UpdateLock(@lock, cancellationToken);
        return new OpenLockResult
        {
            Data = new OpenLockDto
            {
                LockIsOpened = true
            }
        };
    }
}