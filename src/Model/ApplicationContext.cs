using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models.Entities;

namespace Model;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role{Id = Guid.Parse("6b22b9a0-bc42-4b88-96ef-a0c4539d6dac"), Name = "admin"}, new Role{Id = Guid.Parse("2e1d11bf-e1f6-4eb8-9042-5fd6a4bbf6e2"), Name = "user"});
        modelBuilder.Entity<User>().HasData(new User { Id = Guid.Parse("02387541-4280-4d14-86d9-9386310de6ce"), UserName = "admin", PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"});
        modelBuilder.Entity<User>().HasData(new User { Id = Guid.Parse("a96fe69e-cc4c-45a8-a05d-dc498368a766"), UserName = "user", PasswordHash = "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb"});
        modelBuilder.Entity<UserRole>().HasData(new UserRole{Id = Guid.NewGuid(), UserId = Guid.Parse("02387541-4280-4d14-86d9-9386310de6ce"), RoleId = Guid.Parse("6b22b9a0-bc42-4b88-96ef-a0c4539d6dac")});
        modelBuilder.Entity<UserRole>().HasData(new UserRole{Id = Guid.NewGuid(),  UserId = Guid.Parse("a96fe69e-cc4c-45a8-a05d-dc498368a766"), RoleId = Guid.Parse("2e1d11bf-e1f6-4eb8-9042-5fd6a4bbf6e2")});
        modelBuilder.Entity<Key>().HasData(new Key{Id = Guid.Parse("0b209a41-72a5-4572-89b6-3ffa7b9f994f"), Type = KeyTypeEnum.Tag});
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Lock> Locks { get; set; }
    public DbSet<OpeningHistory> OpeningHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<KeyLock> KeyLocks { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<LockSetting> LockSettings { get; set; }
    public DbSet<UserLock> UserLocks { get; set; }
    public DbSet<AccessLock> AccessLocks { get; set; }
}