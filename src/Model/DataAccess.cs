using Microsoft.EntityFrameworkCore;
using Model.Models.Entities;

namespace Model;

public interface IDataAccess
{
    Task<User> GetUser(string userName, CancellationToken token);
    Task<UserRole> GetUserRole(Guid userId, CancellationToken token);
    Task<Role> GetRole(Guid roleId, CancellationToken token);
}

public class DataAccess : IDataAccess, IDisposable
{
    private readonly ApplicationContext _context;

    public DataAccess(ApplicationContext context)
    {
        _context = context;
    }

    public Task<User> GetUser(string userName, CancellationToken token)
        => _context.Users.FirstOrDefaultAsync(u => u.UserName == userName, token);

    public Task<UserRole> GetUserRole(Guid userId, CancellationToken token)
        => _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId, token);

    public Task<Role> GetRole(Guid roleId, CancellationToken token)
        => _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId, token);

    public void Dispose()
    {
        _context?.Dispose();
    }
}