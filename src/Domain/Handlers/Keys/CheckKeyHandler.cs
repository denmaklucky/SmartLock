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

        var expiredAt = key.ExpiredAt.GetValueOrDefault();
        if (expiredAt != default)
        {
            if (IsExpired(expiredAt))
                return new CheckKeyResult { ErrorCode = ErrorCodes.NotActive };
        }

        return new CheckKeyResult();
    }

    private bool IsExpired(DateTime date)
    {
        var now = DateTime.UtcNow;
        //Less than zero if `now` is earlier than `date`
        return DateTime.Compare(now, date) >= 0;
    }
}