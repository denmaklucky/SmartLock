using Microsoft.EntityFrameworkCore;
using Model.Models.Entities;

namespace Model;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Lock> Locks { get; set; }
    public DbSet<OpeningHistory> OpeningHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserLock> UserLocks { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}