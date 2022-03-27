using Microsoft.EntityFrameworkCore;
using Model.Models.Entities;

namespace Model;

public interface IDataAccess
{
    Task<User> GetUser(string userName, CancellationToken token);
    Task<UserRole> GetUserRole(Guid userId, CancellationToken token);
    Task<Role> GetRole(Guid roleId, CancellationToken token);
    Task<Lock> AddLock(Lock @lock, CancellationToken token);
    Task<LockSetting> AddSetting(LockSetting lockSetting, CancellationToken token);
    Task<User> GetUserById(Guid userId, CancellationToken token);
    Task<Lock> UpdateLock(Lock @lock, CancellationToken token);
    Task<Lock> GetLock(Guid lockId, CancellationToken token);
    Task<Key> GetKey(Guid keyId, CancellationToken token);
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

    public Task<User> GetUserById(Guid userId, CancellationToken token)
        => _context.Users.FirstOrDefaultAsync(u => u.Id == userId, token);

    public Task<UserRole> GetUserRole(Guid userId, CancellationToken token)
        => _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId, token);

    public Task<Role> GetRole(Guid roleId, CancellationToken token)
        => _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId, token);

    public async Task<Lock> AddLock(Lock @lock, CancellationToken token)
    {
        var entityLock = await _context.Locks.AddAsync(@lock, token);
        await _context.SaveChangesAsync(token);

        return entityLock.Entity;
    }

    public async Task<LockSetting> AddSetting(LockSetting lockSetting, CancellationToken token)
    {
        var entityLockSetting = await _context.LockSettings.AddAsync(lockSetting, token);
        await _context.SaveChangesAsync(token);

        return entityLockSetting.Entity;
    }

    public async Task<Lock> UpdateLock(Lock @lock, CancellationToken token)
    {
        var entityLock = _context.Locks.Update(@lock);
        await _context.SaveChangesAsync(token);

        return entityLock.Entity;
    }

    public Task<Lock> GetLock(Guid lockId, CancellationToken token)
        => _context.Locks.FirstOrDefaultAsync(l => l.Id == lockId, token);

    public Task<Key> GetKey(Guid keyId, CancellationToken token)
        => _context.Keys.FirstOrDefaultAsync(k => k.Id == keyId, token);

    public void Dispose()
    {
        _context?.Dispose();
    }
}