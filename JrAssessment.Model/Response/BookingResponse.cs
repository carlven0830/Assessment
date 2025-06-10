using JrAssessment.Model.Database;

namespace JrAssessment.Model.Response
{
    public class BookingResponse : Entity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid GuestId { get; set; }
        public TblGuest TblGuest { get; set; } = null!;
        public ICollection<TblRoom> TblRooms { get; set; } = [];
    }
}
