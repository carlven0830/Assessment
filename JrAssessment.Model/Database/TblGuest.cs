using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Database
{
    public class TblGuest : Entity
    {
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;
        public string GuestPhone { get; set; } = string.Empty;
        public DateTime GuestBirthDate { get; set; }
        public string GuestUsername { get; set; } = string.Empty;
        public string GuestPassword { get; set; } = string.Empty;
        public ICollection<TblBooking> TblBookings { get; set; } = [];
    }
}
