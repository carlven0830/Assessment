using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Requests
{
    public class UpdateProjectRequest : AddProjectRequest
    {
        public string Id { get; set; } = string.Empty;
        public StatusEnum Status { get; set; }
    }
}
