using JrAssessment.Model.Database;

namespace JrAssessment.Model.Response
{
    public class RoomResponse : Entity
    {
        public string RoomNum { get; set; }  = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
