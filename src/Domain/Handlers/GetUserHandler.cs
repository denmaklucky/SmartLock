using Domain.Queries;
using Domain.Results;
using MediatR;
using Model;

namespace Domain.Handlers;

public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResult>
{
    private readonly IDataAccess _dataAccess;

    public GetUserHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public async Task<GetUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dataAccess.GetUserById(request.UserId, cancellationToken);

        return user == null 
            ? new GetUserResult { ErrorCode = ErrorCodes.UserNotFound } 
            : new GetUserResult { UserId = user.Id, UserName = user.UserName };
    }
}