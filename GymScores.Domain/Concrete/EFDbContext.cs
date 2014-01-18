using System.Data.Entity;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // allows EF to save proper decimale precision
            modelBuilder.Entity<Score>().Property(Score => Score.AllAround).HasPrecision(5, 3);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Meet> Meets { get; set; }
        public DbSet<Gymnast> Gymnasts { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
