using DatabaseModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModel
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Intermediate Tables

            #region Roles_Claims

            modelBuilder.Entity<Role>().HasMany(r => r.Claims).WithMany(a => a.Roles)
                .UsingEntity<Dictionary<string, object>>("Roles_Claims",
                    j => j.HasOne<Claim>().WithMany().HasForeignKey("ClaimId").OnDelete(DeleteBehavior.ClientCascade),
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientCascade));

            #endregion

            #region Users_Roles

            modelBuilder.Entity<User>().HasMany(r => r.Roles).WithMany(a => a.Users)
                .UsingEntity<Dictionary<string, object>>("Users_Roles",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientCascade),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientCascade));

            #endregion

            #endregion
        }
    }
}