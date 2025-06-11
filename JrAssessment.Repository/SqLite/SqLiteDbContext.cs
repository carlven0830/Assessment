using JrAssessment.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace JrAssessment.Repository.SqLite
{
    public class SqLiteDbContext : DbContext
    {
        public SqLiteDbContext(DbContextOptions<SqLiteDbContext> options) : base(options) {}

        // register the entities here
        public DbSet<TblEmployee> TblEmployee { get; set; }
        public DbSet<TblProject> TblProject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblProject>()
                .HasMany(x => x.TblEmployees)
                .WithMany(x => x.TblProjects);
        }
    }
}
