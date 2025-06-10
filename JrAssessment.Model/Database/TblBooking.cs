using JrAssessment.Model.Database.JoinTable;
using JrAssessment.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Database
{
    public class TblBooking : Entity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatusEnum Status { get; set; }
        public Guid GuestId { get; set; }
        public TblGuest TblGuest { get; set; } = null!;
        public ICollection<TblRoom> TblRooms { get; set; } = [];
    }
}
