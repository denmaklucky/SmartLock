using Domain.Commands.Keys;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Results.Keys;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Keys;

public class DeleteKeyHandler : IRequestHandler<DeleteKeyCommand, DeleteKeyResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<DeleteKeyCommand> _validator;

    public DeleteKeyHandler(IDataAccess dataAccess, IMediator mediator, IValidator<DeleteKeyCommand> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public async Task<DeleteKeyResult> Handle(DeleteKeyCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new DeleteKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.DeletedBy), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.DeletedBy}");

        var keyId = Guid.Parse(request.KeyId);
        var checkKeyResult = await _mediator.Send(new CheckKeyCommand(keyId), cancellationToken);

        if (!checkKeyResult.IsSuccess)
            return new DeleteKeyResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = new []{$"Key with id {keyId} is not valid. Please try different one."}};

        var key = await _dataAccess.GetKey(keyId, cancellationToken);

        key.IsDeleted = true;
        key.ModifiedBy = request.DeletedBy;
        key.ModifiedOn = DateTime.UtcNow;

        var updatedKey = await _dataAccess.UpdateKey(key, cancellationToken);

        return new DeleteKeyResult
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