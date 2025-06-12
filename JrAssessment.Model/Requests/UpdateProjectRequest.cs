using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Requests
{
    public class UpdateProjectRequest : AddProjectRequest
    {
        public Guid Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
