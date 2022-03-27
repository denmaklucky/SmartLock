using Domain.Commands;
using Domain.Results;
using FluentValidation;
using MediatR;
using Model;

namespace Domain.Handlers;

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
    
    public Task<OpenLockResult> Handle(OpenLockCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}