using Domain.Dto;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Queries.Locks;
using Domain.Results.Locks;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers.Locks;

public class GetLocksHandler : IRequestHandler<GetLocksQuery, GetLocksResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMediator _mediator;
    private readonly IValidator<GetLocksQuery> _validator;

    public GetLocksHandler(IDataAccess dataAccess, IMediator mediator, IValidator<GetLocksQuery> validator)
    {
        _dataAccess = dataAccess;
        _mediator = mediator;
        _validator = validator;
    }
    
    public async Task<GetLocksResult> Handle(GetLocksQuery request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new GetLocksResult { ErrorCode = ErrorCodes.InvalidRequest, Messages = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray() };

        var getUserResult = await _mediator.Send(new GetUserQuery(request.UserId), cancellationToken);

        if (!getUserResult.IsSuccess)
            throw new LogicException(ErrorCodes.InternalError, $"Couldn't find an user by following `userId` {request.UserId}");

        var locks = await _dataAccess.GetLockByUserId(request.UserId, cancellationToken);
        return new GetLocksResult
        {
            Data = locks.Select(l => new LockDto
            {
                Id = l.Id,
                State = l.State,
                Title = l.Title,
                Setting = new LockSettingDto
                {
                    Mode = l.Setting.Mode,
                    EndOpenTime = l.Setting.EndOpenTime,
                    StartOpenTime = l.Setting.StartOpenTime
                }
            }).ToArray()
        };
    }
}