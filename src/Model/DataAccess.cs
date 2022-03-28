﻿using Microsoft.EntityFrameworkCore;
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
    Task<IEnumerable<Lock>> GetLocksByUserId(Guid userId, CancellationToken token);
    Task<Key> AddKey(Key key, CancellationToken token);
    Task<AccessLock> AddAccessLock(AccessLock accessLock, CancellationToken token);
    Task<AccessLock> GetAccessLock(Guid accessId, Guid lockId, CancellationToken token);
    Task<AccessLock> UpdateAccessLock(AccessLock accessLock, CancellationToken token);
    Task<Key> UpdateKey(Key key, CancellationToken token);
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

    public async Task<IEnumerable<Lock>> GetLocksByUserId(Guid userId, CancellationToken token)
    {
        var getLocksByUserIdQuery =
            from al in _context.AccessLocks
            join l in _context.Locks.Include(l => l.Setting) on al.LockId equals l.Id
            where l.CreatedBy == userId
            select l;

        var getLockByKeyQuery =
            from key in _context.Keys
            join al in _context.AccessLocks on key.Id equals al.AccessId
            join l in _context.Locks.Include(l => l.Setting) on al.LockId equals l.Id
            where key.UserId == userId
            select l;

        return getLocksByUserIdQuery.Union(getLockByKeyQuery).Distinct();
    }

    public async Task<Key> AddKey(Key key, CancellationToken token)
    {
        var entityKey = await _context.Keys.AddAsync(key, token);
        await _context.SaveChangesAsync(token);

        return entityKey.Entity;
    }

    public async Task<AccessLock> AddAccessLock(AccessLock accessLock, CancellationToken token)
    {
        var entityAccessLock =await _context.AccessLocks.AddAsync(accessLock, token);
        await _context.SaveChangesAsync(token);

        return entityAccessLock.Entity;
    }

    public Task<AccessLock> GetAccessLock(Guid accessId, Guid lockId, CancellationToken token)
        => _context.AccessLocks.FirstOrDefaultAsync(al => al.AccessId == accessId && al.LockId == lockId, token);
    
    public async Task<AccessLock> UpdateAccessLock(AccessLock accessLock, CancellationToken token)
    {
        var entityLock = _context.AccessLocks.Update(accessLock);
        await _context.SaveChangesAsync(token);

        return entityLock.Entity;
    }
    
    public async Task<Key> UpdateKey(Key key, CancellationToken token)
    {
        var entityKey = _context.Keys.Update(key);
        await _context.SaveChangesAsync(token);

        return entityKey.Entity;
    }

    public void Dispose()
        => _context?.Dispose();
}