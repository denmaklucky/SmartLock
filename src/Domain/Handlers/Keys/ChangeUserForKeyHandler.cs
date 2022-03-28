using Domain.Commands.Keys;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Keys;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Keys;

public class ChangeUserForKeyHandler : IRequestHandler<ChangeUserForKeyCommand, ChangeUserForKeyResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<ChangeUserForKeyCommand> _validator;

    public ChangeUserForKeyHandler(IDataAccess dataAccess, IMediator mediator, IValidator<ChangeUserForKeyCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public async Task<ChangeUserForKeyResult> Handle(ChangeUserForKeyCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new ChangeUserForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUpdatedByResult = await _mediator.Send(new GetUserQuery(request.UpdatedBy), cancellationToken);

        if (!getUpdatedByResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UpdatedBy}");

        var keyId = Guid.Parse(request.KeyId);
        var checkKeyResult = await _mediator.Send(new CheckKeyCommand(keyId), cancellationToken);

        if (!checkKeyResult.IsSuccess)
            return new ChangeUserForKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{$"Key with id {keyId} is not valid. Please try different one."}};

        var userId = Guid.Parse(request.NewUserId);
        var getUserResult = await _mediator.Send(new GetUserQuery(userId), cancellationToken);

        if (!getUserResult.IsSuccess)
            return new ChangeUserForKeyResult{ ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{ $"Couldn't find an user by following `userId` {userId}"}};

        var key = await _dataAccess.GetKey(keyId, cancellationToken);
        
        key.UserId = userId;
        key.ModifiedBy = request.UpdatedBy;
        key.ModifiedOn = DateTime.UtcNow;

        var updatedKey = await _dataAccess.UpdateKey(key, cancellationToken);

        return new ChangeUserForKeyResult
        {
            Data = new KeyDto
            {
                Id = updatedKey.Id,
                Type = updatedKey.Type,
                IsDeleted = updatedKey.IsDeleted,
                UserId = updatedKey.UserId
            }
        };
    }
}