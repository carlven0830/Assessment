using JrAssessment.Model.Enums;

namespace JrAssessment.Model.Database
{
    public class TblEmployee : Account
    {
        public string EmpName { get; set; } = string.Empty;
        public PositionEnum EmpPosition { get; set; }
        public EmpLevelEnum EmpLevel { get; set; }
        public ICollection<TblProject> TblProjects { get; set; } = [];
    }
}
