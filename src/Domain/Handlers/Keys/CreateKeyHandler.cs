﻿using Domain.Commands.Keys;
using Domain.Commands.Locks;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Keys;
using FluentValidation;
using MediatR;
using Model;
using Model.Enums;
using Model.Models.Entities;

namespace Domain.Handlers.Keys;

public class CreateKeyHandler : IRequestHandler<CreateKeyCommand, CreateKeyResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<CreateKeyCommand> _validator;

    public CreateKeyHandler(IDataAccess dataAccess, IMediator mediator, IValidator<CreateKeyCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<CreateKeyResult> Handle(CreateKeyCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new CreateKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserByCreatedByResult = await _mediator.Send(new GetUserQuery(request.CreatedBy), cancellationToken);

        if (!getUserByCreatedByResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `CreatedBy` {request.CreatedBy}");

        var userId = Guid.Parse(request.UserId);
        var getUserByUserIdResult = await _mediator.Send(new GetUserQuery(userId), cancellationToken);

        if (!getUserByUserIdResult.IsSuccess)
            return new CreateKeyResult { ErrorCode = ErrorCodes.NotFound, Messages = new[] { $"Couldn't find an user by following `UserId` {userId}"} };

        var lockId = Guid.Parse(request.LockId);
        var checkLockResult = await _mediator.Send(new CheckLockCommand(lockId), cancellationToken);

        if (!checkLockResult.IsSuccess)
            return new CreateKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { $"Lock with id `{lockId}` not found."} };

        var newKey = new Key
        {
            Type = request.Type,
            UserId = userId,
            CreatedBy = request.CreatedBy,
            CreatedOn = DateTime.UtcNow
        };

        var createdKey = await _dataAccess.AddKey(newKey, cancellationToken);

        var accessLock = new AccessLock
        {
            Type = AccessTypeEnum.Key,
            AccessId = createdKey.Id,
            LockId = lockId
        };
        await _dataAccess.AddAccessLock(accessLock, cancellationToken);

        return new CreateKeyResult
        {
            Data = new KeyDto
            {
                Id = createdKey.Id,
                Type = createdKey.Type,
                UserId = createdKey.UserId,
                IsDeleted = createdKey.IsDeleted
            }
        };
    }
}