using Domain.Commands.Keys;
using Domain.Results.Keys;
using MediatR;
using Model;

namespace Domain.Handlers.Keys;

public class CheckKeyHandler : IRequestHandler<CheckKeyCommand, CheckKeyResult>
{
    private readonly IDataAccess _dataAccess;

    public CheckKeyHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<CheckKeyResult> Handle(CheckKeyCommand request, CancellationToken cancellationToken)
    {
        var key = await _dataAccess.GetKey(request.KeyId, cancellationToken);

        if (key == null)
            return new CheckKeyResult { ErrorCode = ErrorCodes.NotFound };

        if (key.IsDeleted)
            return new CheckKeyResult { ErrorCode = ErrorCodes.NotActive };

        return new CheckKeyResult();
    }
}