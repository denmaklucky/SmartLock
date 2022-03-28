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

public class AdmitLockHandler : IRequestHandler<AdmitLockCommand, AdmitLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<AdmitLockCommand> _validator;

    public AdmitLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<AdmitLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<AdmitLockResult> Handle(AdmitLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new AdmitLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };
        
        var getAdmittedByResult = await _mediator.Send(new GetUserQuery(request.AdmittedBy), cancellationToken);

        if (!getAdmittedByResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.AdmittedBy}");

        var accessId = Guid.Parse(request.AccessId);

        if (request.Type == AccessTypeEnum.Key)
        {
            var checkKeyResult = await _mediator.Send(new CheckKeyCommand(accessId), cancellationToken);

            if (!checkKeyResult.IsSuccess)
                return new AdmitLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{$"Key with id {accessId} is not valid. Please try different one."}};
        }
        else
        {
            var getUserResult = await _mediator.Send(new GetUserQuery(accessId), cancellationToken);

            if (!getUserResult.IsSuccess)
                return new AdmitLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{$"Couldn't find an user by following `userId` {accessId}"}};
        }

        var lockId = Guid.Parse(request.LockId);
        var checkLockResult = await _mediator.Send(new CheckLockCommand(lockId), cancellationToken);

        if (!checkLockResult.IsSuccess)
            return new AdmitLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new[] { $"Lock with id `{lockId}` not found."} };

        var accessLock = new AccessLock
        {
            Type = request.Type,
            AccessId = accessId,
            LockId = lockId,
            CreatedBy = request.AdmittedBy,
            CreatedOn = DateTime.UtcNow
        };

        var addedAccessLock = await _dataAccess.AddAccessLock(accessLock, cancellationToken);

        return new AdmitLockResult
        {
            Data = new AccessLockDto
            {
                Id = addedAccessLock.Id,
                Type = accessLock.Type,
                AccessId = addedAccessLock.AccessId,
                IsDeleted = addedAccessLock.IsDeleted,
                LockId = addedAccessLock.LockId
            }
        };
    }
}