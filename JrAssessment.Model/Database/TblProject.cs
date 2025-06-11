using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Database
{
    public class TblProject : Entity
    {
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public StatusEnum Status { get; set; }
        public ICollection<TblEmployee> TblEmployees { get; set; } = [];
    }
}
