using JrAssessment.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Request
{
    public class AddRoomRequest
    {
        public RoomTypeEnum RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
