using JrAssessment.Model.Database;
using JrAssessment.Model.Responses;

namespace JrAssessment.Core.Mapping
{
    public static class ManualMapping
    {
        public static EmployeeResponse MapToEmployeeResp(this TblEmployee employee)
        {
            return new EmployeeResponse
            {
                Id           = employee.Id,
                CreateBy     = employee.CreateBy,
                CreateDate   = employee.CreateDate,
                ModifiedBy   = employee.ModifiedBy,
                ModifiedDate = employee.ModifiedDate,
                IsEnabled    = employee.IsEnabled,
                Username     = employee.Username,
                Password     = employee.Password,
                Email        = employee.Email,
                Phone        = employee.Phone,
                SessionKey   = employee.SessionKey,
                EmpName      = employee.EmpName,
                EmpPosition  = employee.EmpPosition.ToString(),
                EmpLevel     = employee.EmpLevel.ToString()
            };
        }

        public static List<EmployeeResponse> MapToEmployeeListResp(this List<TblEmployee> employees)
        {
            return employees.Select(x => new EmployeeResponse
            {
                Id = x.Id,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsEnabled = x.IsEnabled,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                Phone = x.Phone,
                SessionKey = x.SessionKey,
                EmpName = x.EmpName,
                EmpPosition = x.EmpPosition.ToString(),
                EmpLevel = x.EmpLevel.ToString()
            }).ToList();
        }

        public static ProjectResponse MapToProjectResp(this TblProject project)
        {
            return new ProjectResponse
            {
                Id                 = project.Id,
                CreateBy           = project.CreateBy,
                CreateDate         = project.CreateDate,
                ModifiedBy         = project.ModifiedBy,
                ModifiedDate       = project.ModifiedDate,
                IsEnabled          = project.IsEnabled,
                ProjectTitle       = project.ProjectTitle,
                ProjectDescription = project.ProjectDescription,
                Status             = project.Status.ToString(),
                AssignedEmpName    = project.TblEmployees.Select(e => e.EmpName).ToList()
            };
        }

        public static List<ProjectResponse> MapToProjectListResp(this List<TblProject> projects)
        {
            return projects.Select(x => new ProjectResponse
            {
                Id                 = x.Id,
                CreateBy           = x.CreateBy,
                CreateDate         = x.CreateDate,
                ModifiedBy         = x.ModifiedBy,
                ModifiedDate       = x.ModifiedDate,
                IsEnabled          = x.IsEnabled,
                ProjectTitle       = x.ProjectTitle,
                ProjectDescription = x.ProjectDescription,
                Status             = x.Status.ToString(),
                AssignedEmpName    = x.TblEmployees.Select(e => e.EmpName).ToList()
            }).ToList();
        }
    }
}
