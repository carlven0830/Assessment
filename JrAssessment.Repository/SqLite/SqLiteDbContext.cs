using JrAssessment.Model.Database;
using JrAssessment.Model.Database.JoinTable;
using Microsoft.EntityFrameworkCore;

namespace JrAssessment.Repository.SqLite
{
    public class SqLiteDbContext : DbContext
    {
        public SqLiteDbContext(DbContextOptions<SqLiteDbContext> options) : base(options) {}

        // register the entities here
        public DbSet<TblGuest> TblGuest { get; set; }
        public DbSet<TblRoom> TblRoom { get; set; }
        public DbSet<TblBooking> TblBooking { get; set; }
        public DbSet<TblBookingRoom> TblBookingRoom { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblGuest>()
                .HasMany(x => x.TblBookings)
                .WithOne(x => x.TblGuest)
                .HasForeignKey(x => x.GuestId);

            modelBuilder.Entity<TblBooking>()
                .HasMany(x => x.TblRooms)
                .WithMany(x => x.TblBookings)
                .UsingEntity<TblBookingRoom>();
        }
    }
}
