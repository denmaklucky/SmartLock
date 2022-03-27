using Domain.Commands.Locks;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;
using Model.Enums;

namespace Domain.Handlers.Locks;

public class DeleteLockHandler : IRequestHandler<DeleteLockCommand, DeleteLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<DeleteLockCommand> _validator;

    public DeleteLockHandler(IDataAccess dataAccess, IMediator mediator, IValidator<DeleteLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<DeleteLockResult> Handle(DeleteLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new DeleteLockResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var @lock = await _dataAccess.GetLock(request.LockId, cancellationToken);

        if (@lock == null)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find a lock by following `lockId` {request.LockId}");

        @lock.IsDeleted = true;
        @lock.State = LockStateEnum.Offline;
        @lock.ModifiedBy = request.UserId;
        @lock.ModifiedOn = DateTime.UtcNow;

        var updatedLock = await _dataAccess.UpdateLock(@lock, cancellationToken);

        return new DeleteLockResult
        {
            Data = new DeleteLockDto
            {
                IsDelete = updatedLock.IsDeleted,
                LockId = updatedLock.Id
            }
        };
    }
}