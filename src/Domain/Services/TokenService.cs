using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Models.Entities;

namespace Domain.Services;

public interface ITokenService
{
    string GenerateBearerToken(User user, Role role);
}

public class TokenService : ITokenService
{
    private readonly TokenOptions _options;
    
    public TokenService(IOptions<TokenOptions> options)
    {
        _options = options.Value;
    }
    
    public string GenerateBearerToken(User user, Role role)
    {
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            notBefore: now,
            claims: GetClaimsIdentity(user, role).Claims,
            expires: now.AddSeconds(_options.TokenLifetime),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.ClientSecret)), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
    private ClaimsIdentity GetClaimsIdentity(User user, Role role)
    {
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("role", role.Name)
        };

        return new ClaimsIdentity(claims);
    }
}