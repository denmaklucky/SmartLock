using Domain.Commands.Keys;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Keys;
using FluentValidation;
using MediatR;
using Model;
using Model.Models.Entities;

namespace Domain.Handlers.Keys;

public class AssignKeyToUserHandler : IRequestHandler<AssignKeyToUserCommand, AssignKeyToUserResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<AssignKeyToUserCommand> _validator;

    public AssignKeyToUserHandler(IDataAccess dataAccess, IMediator mediator, IValidator<AssignKeyToUserCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<AssignKeyToUserResult> Handle(AssignKeyToUserCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new AssignKeyToUserResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var assignToId = Guid.Parse(request.AssignTo);
        var getAssignUserResult = await _mediator.Send(new GetUserQuery(assignToId), cancellationToken);

        if (!getAssignUserResult.IsSuccess)
            return new AssignKeyToUserResult { ErrorCode = ErrorCodes.NotFound, Messages = new[] { $"Couldn't find an user for assign with id `{assignToId}`" } };
        
        //await _dataAccess.AddKeyLock(new KeyLock { KeyId = createdKey.Id, LockId = lockId }, cancellationToken);

        return new AssignKeyToUserResult();
    }
}