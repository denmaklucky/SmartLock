using Domain.Commands;
using Domain.Results;
using FluentValidation;
using MediatR;
using Model;

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
            return new CreateLockResult { ErrorCode = "" };
        
        //Create a lock
        //Create a default settings
        return new CreateLockResult();
    }
}