using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Requests
{
    public class UpdateRoomRequest : AddProjectRequest
    {
        public Guid Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
