using Microsoft.EntityFrameworkCore;
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
        modelBuilder.Entity<User>().HasData(new User { Id = Guid.Parse("02387541-4280-4d14-86d9-9386310de6ce"), UserName = "john", PasswordHash = "96d9632f363564cc3032521409cf22a852f2032eec099ed5967c0d000cec607a"});
        modelBuilder.Entity<User>().HasData(new User { Id = Guid.Parse("a96fe69e-cc4c-45a8-a05d-dc498368a766"), UserName = "victoria", PasswordHash = "ab1cb712f2dca756105160805501f4d6d8657d93d40b16eee4ecb5fd048d26eb"});
        modelBuilder.Entity<UserRole>().HasData(new UserRole{Id = Guid.NewGuid(), UserId = Guid.Parse("02387541-4280-4d14-86d9-9386310de6ce"), RoleId = Guid.Parse("6b22b9a0-bc42-4b88-96ef-a0c4539d6dac")});
        modelBuilder.Entity<UserRole>().HasData(new UserRole{Id = Guid.NewGuid(),  UserId = Guid.Parse("a96fe69e-cc4c-45a8-a05d-dc498368a766"), RoleId = Guid.Parse("2e1d11bf-e1f6-4eb8-9042-5fd6a4bbf6e2")});
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Lock> Locks { get; set; }
    public DbSet<OpeningHistory> OpeningHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserLock> UserLocks { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}