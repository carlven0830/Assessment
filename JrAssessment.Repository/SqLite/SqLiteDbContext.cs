using JrAssessment.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace JrAssessment.Repository.SqLite
{
    public class SqLiteDbContext : DbContext
    {
        public SqLiteDbContext(DbContextOptions options) : base(options) {}

        // register the entities here
        public DbSet<TblProduct> Product { get; set; }
    }
}
