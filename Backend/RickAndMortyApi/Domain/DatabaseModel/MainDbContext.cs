using DatabaseModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModel
{
    public class MainDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Intermediate Tables

            #region Characters_Episodes

            modelBuilder.Entity<Character>().HasMany(r => r.Episodes).WithMany(a => a.Characters)
                .UsingEntity<Dictionary<string, object>>("Characters_Episodes",
                    j => j.HasOne<Episode>().WithMany().HasForeignKey("EpisodeId").OnDelete(DeleteBehavior.ClientCascade),
                    j => j.HasOne<Character>().WithMany().HasForeignKey("CharacterId").OnDelete(DeleteBehavior.ClientCascade));

            #endregion

            #endregion
        }
    }
}