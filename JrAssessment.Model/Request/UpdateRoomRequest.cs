using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Request
{
    public class UpdateRoomRequest : AddRoomRequest
    {
        public Guid Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
