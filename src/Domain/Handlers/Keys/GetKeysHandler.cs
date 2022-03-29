using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Queries.Keys;
using Domain.Results.Keys;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Keys;

public class GetKeysHandler : IRequestHandler<GetKeysQuery, GetKeysResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<GetKeysQuery> _validator;

    public GetKeysHandler(IDataAccess dataAccess, IMediator mediator, IValidator<GetKeysQuery> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<GetKeysResult> Handle(GetKeysQuery request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new GetKeysResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.CreatedBy), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.CreatedBy}");

        var keys = _dataAccess.GetKeysByCreatedUser(request.CreatedBy);
        return new GetKeysResult
        {
            Data = keys.Select(k => new KeyDto
            {
                Id = k.Id,
                Type = k.Type,
                IsDeleted = k.IsDeleted,
                UserId = k.UserId
            }).ToArray() 
        };
    }
}