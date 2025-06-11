using JrAssessment.Model.Base;
using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Requests
{
    public class FilterProjectRequest : ListRequest
    {
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public StatusEnum Status { get; set; }
    }
}
