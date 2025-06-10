using JrAssessment.Model.Database;
using JrAssessment.Model.Response;
using Riok.Mapperly.Abstractions;

namespace JrAssessment.Core.Mapping
{
    [Mapper]
    public static partial class ModelMapping
    {
        public static partial RoomResponse MapToRoomResp(this TblRoom room);
        public static partial BookingResponse MapToBookingResp(this TblBooking booking);
    }
}
