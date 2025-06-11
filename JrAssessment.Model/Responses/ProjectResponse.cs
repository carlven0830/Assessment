using JrAssessment.Model.Database;
using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Responses
{
    public class ProjectResponse : Entity
    {
        public string ProjectTitle { get; set; }       = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public string Status { get; set; }             = string.Empty;
    }
}
