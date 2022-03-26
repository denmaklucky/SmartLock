using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;

namespace WebApi.Extensions;

public static class PrincipalExtensions
{
    public static Guid GetUserId(this IPrincipal user)
    {
        var identity = GetClaimsIdentity(user);
        var claim = identity.FindFirst("id");
        return claim == null ? throw new AuthenticationException("Not found `id` in claims") : Guid.Parse(claim.Value);
    }
    
    private static ClaimsIdentity GetClaimsIdentity(IPrincipal user)
    {
        return user.Identity switch
        {
            ClaimsIdentity { IsAuthenticated: true } identity => identity,
            _ => throw new AuthenticationException("No user is signed in.")
        };
    }
}