using Domain.Commands;
using Domain.Results;
using FluentValidation;
using MediatR;
using Model;
using Model.Enums;
using Model.Models.Entities;

namespace Domain.Handlers;

public class CreateLockHandler : IRequestHandler<CreateLockCommand, CreateLockResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IValidator<CreateLockCommand> _validator;

    public CreateLockHandler(IDataAccess dataAccess, IValidator<CreateLockCommand> validator)
    {
        _dataAccess = dataAccess;
        _validator = validator;
    }

    public async Task<CreateLockResult> Handle(CreateLockCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            return new CreateLockResult { ErrorCode = ErrorCodes.InvalidRequest, ValidatorErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToArray()};

        //Create a lock
        var @lock = new Lock
        {
            State = LockStateEnum.Online,
            Title = request.Title,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.UserId
        };
        

        //Create a default settings
        return new CreateLockResult();
    }
}