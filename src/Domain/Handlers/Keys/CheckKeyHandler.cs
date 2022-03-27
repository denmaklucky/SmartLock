using Domain.Commands.Keys;
using Domain.Exceptions;
using Domain.Results.Keys;
using MediatR;
using Model;
using Model.Enums;

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
            return new CheckKeyResult { ErrorCode =  ErrorCodes.NotFound};

        if (key.IsDeleted)
            return new CheckKeyResult {ErrorCode = ErrorCodes.NotActive};

        if (key.Type == KeyTypeEnum.eKey)
        {
            var expiredAt = key.ExpiredAt.GetValueOrDefault();
            
            if (expiredAt == default)
                throw new LogicException(ErrorCodes.InternalError, $"eKey with id {key.Id} doesn't contains an expiry date");

            if (IsExpired(expiredAt))
                return new CheckKeyResult {ErrorCode = ErrorCodes.NotActive};
        }

        return new CheckKeyResult();
    }

    private bool IsExpired(DateTime date)
    {
        var now = DateTime.UtcNow;
        //Less than zero if `now` is earlier than `date`
        return  DateTime.Compare(now, date) >= 0;
    }
}