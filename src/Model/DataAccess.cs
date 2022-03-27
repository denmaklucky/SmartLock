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
    Task<IEnumerable<Lock>> GetLockByUserId(Guid userId, CancellationToken token);
    Task AddUserLock(UserLock userLock, CancellationToken token);
    Task<Key> AddKey(Key key, CancellationToken token);
    Task AddKeyLock(KeyLock keyLock, CancellationToken token);
    Task<KeyLock> GetKeyLock(Guid keyId, Guid lockId, CancellationToken token);
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

    public async Task<IEnumerable<Lock>> GetLockByUserId(Guid userId, CancellationToken token)
    {
        var query =
            from ul in _context.UserLocks
            join l in _context.Locks.Include(l => l.Setting) on ul.LockId equals l.Id
            where l.CreatedBy == userId
            select l;

        return query;
    }

    public async Task AddUserLock(UserLock userLock, CancellationToken token)
    {
        await _context.UserLocks.AddAsync(userLock, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task<Key> AddKey(Key key, CancellationToken token)
    {
        var entityKey = await _context.Keys.AddAsync(key, token);
        await _context.SaveChangesAsync(token);

        return entityKey.Entity;
    }

    public async Task AddKeyLock(KeyLock keyLock, CancellationToken token)
    {
        await _context.KeyLocks.AddAsync(keyLock, token);
        await _context.SaveChangesAsync(token);
    }

    public Task<KeyLock> GetKeyLock(Guid keyId, Guid lockId, CancellationToken token)
        => _context.KeyLocks.FirstOrDefaultAsync(kl => kl.KeyId == keyId && kl.LockId == lockId, token);

    public void Dispose()
        => _context?.Dispose();
}