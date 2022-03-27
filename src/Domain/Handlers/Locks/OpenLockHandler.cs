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
            return new OpenLockResult { ErrorCode = ErrorCodes.InvalidRequest, ValidatorErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var @lock = await _dataAccess.GetLock(Guid.Parse(request.LockId), cancellationToken);

        if (@lock == null)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find a lock by following `lockId` {request.LockId}");

        if (@lock.IsDeleted || @lock.State == LockStateEnum.Offline)
            throw new LogicException(ErrorCodes.InternalError, $"The lock {@lock.Id} is deleted or offline");
        
        @lock.OpeningHistories.Add(new OpeningHistory
        {
            LockId = @lock.Id,
            KeyId = Guid.Parse(request.KeyId),
            CreatedBy = request.UserId,
            CreatedOn = DateTime.UtcNow
        });

        var updatedLock = await _dataAccess.UpdateLock(@lock, cancellationToken);
        return new OpenLockResult
        {
            Data = new OpenLockDto
            {
                LockIsOpened = true
            }
        };
    }
}