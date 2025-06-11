using JrAssessment.Model.Enums;
namespace JrAssessment.Model.Requests
{
    public class AddProjectRequest
    {
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public ICollection<Guid> EmployeeIds { get; set; } = [];
    }
}
