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
    public class TblRoom : Entity
    {
        public string RoomNum { get; set; } = string.Empty;
        public RoomTypeEnum RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public StatusEnum Status { get; set; }
        public ICollection<TblBooking> TblBookings { get; set; } = [];
    }
}
