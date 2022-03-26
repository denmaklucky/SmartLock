namespace Domain.Services;

public interface ITokenService
{
    string GenerateBearerToken();
}

public class TokenService : ITokenService
{
    public string GenerateBearerToken()
    {
        return string.Empty;
    }
}