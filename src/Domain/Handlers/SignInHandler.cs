using Domain.Commands;
using Domain.Results;
using Domain.Services;
using MediatR;
using Model;

namespace Domain.Handlers;

public class SignInHandler : IRequestHandler<SignInCommand, SignInResult>
{
    private readonly IDataAccess _dataAccess;
    private readonly IHashService _hashService;
    private readonly ITokenService _tokenService;

    public SignInHandler(IDataAccess dataAccess, IHashService hashService, ITokenService tokenService)
    {
        _dataAccess = dataAccess;
        _hashService = hashService;
        _tokenService = tokenService;
    }

    public async Task<SignInResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataAccess.GetUser(request.Login, cancellationToken);
        
        if (user == null)
            return new SignInResult { ErrorCode = ErrorCodes.NotFound };

        var hashPassword = _hashService.Generate(request.ProvidedPassword);
        
        if (!string.Equals(user.PasswordHash, hashPassword))
            return new SignInResult { ErrorCode = ErrorCodes.InvalidPassword };

        var userRole = await _dataAccess.GetUserRole(user.Id, cancellationToken);
        var role = await _dataAccess.GetRole(userRole.RoleId, cancellationToken);

        var bearerToken = _tokenService.GenerateBearerToken(user, role);
        return new SignInResult { AccessToken = bearerToken };
    }
}