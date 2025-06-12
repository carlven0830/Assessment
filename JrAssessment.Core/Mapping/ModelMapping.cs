using JrAssessment.Model.Database;
using JrAssessment.Model.Responses;
using Riok.Mapperly.Abstractions;

namespace JrAssessment.Core.Mapping
{
    [Mapper]
    public static partial class ModelMapping
    {
        public static partial ProjectResponse MapToProjectResp(this TblProject project);
        public static partial List<ProjectResponse> MapToProjectListResp(this List<TblProject> projectList);
        public static partial EmployeeResponse MapToEmployeeResp(this TblEmployee employee);
    }
}
