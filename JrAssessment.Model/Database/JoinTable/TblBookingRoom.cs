using System.ComponentModel.DataAnnotations.Schema;

namespace JrAssessment.Model.Database.JoinTable
{
    public class TblBookingRoom
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
        public TblBooking TblBooking { get; set; } = null!;
        public TblRoom TblRoom { get; set; } = null!;
    }
}
