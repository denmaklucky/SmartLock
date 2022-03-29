using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Queries.Locks;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class GetOpeningHistoryHandler : IRequestHandler<GetOpeningHistoryQuery, GetOpeningHistoryResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<GetOpeningHistoryQuery> _validator;

    public GetOpeningHistoryHandler(IDataAccess dataAccess, IMediator mediator, IValidator<GetOpeningHistoryQuery> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<GetOpeningHistoryResult> Handle(GetOpeningHistoryQuery request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new GetOpeningHistoryResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var openingHistories = _dataAccess.GetOpenHistoriesByUserId(request.UserId);
        return new GetOpeningHistoryResult
        {
            Data = openingHistories.Select(oh => new OpeningHistoryDto
            {
                AccessId = oh.AccessId,
                LockId = oh.LockId,
                OpenedOn = oh.CreatedOn,
                UserName = oh.UserName
            }).ToArray()
        };
    }
}