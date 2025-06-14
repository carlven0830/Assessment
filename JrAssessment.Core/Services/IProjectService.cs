using JrAssessment.Core.Mapping;
using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Enums;
using JrAssessment.Model.Requests;
using JrAssessment.Model.Responses;
using JrAssessment.Model.Settings;
using JrAssessment.Repository.SqLite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace JrAssessment.Core.Services
{
    public interface IProjectService
    {
        Task<ListResponse<ProjectResponse>> GetProjectListAsync(FilterProjectRequest request, ClaimSetting claim);
        Task<ContentResponse<ProjectResponse>> GetProjectAsync(string id);
        Task<ContentResponse<ProjectResponse>> AddProjectAsync(AddProjectRequest request, ClaimSetting claim);
        Task<ContentResponse<ProjectResponse>> UpdateProjectAsync(UpdateProjectRequest request, ClaimSetting claim);
        Task<ContentResponse<ProjectResponse>> DeleteProjectAsync(string id);
    }

    public class ProjectService : BaseService, IProjectService
    {
        private readonly ISqLiteRepo<TblProject> _projectRepo;
        private readonly ISqLiteRepo<TblEmployee> _empRepo;

        public ProjectService(ISqLiteRepo<TblProject> projectRepo, ISqLiteRepo<TblEmployee> empRepo)
        {
            _projectRepo = projectRepo;
            _empRepo     = empRepo;
        }

        public async Task<ListResponse<ProjectResponse>> GetProjectListAsync(FilterProjectRequest request, ClaimSetting claim)
        {
            var listFilter = new List<Expression<Func<TblProject, bool>>>();
            Expression<Func<TblProject, object>> sortOrderBy;
            Func<IQueryable<TblProject>, IQueryable<TblProject>> include = x => x.Include(e => e.TblEmployees);

            var projectList = new List<TblProject>();
            long totalCount = 0;
            long totalPage = 0;

            bool resultAccess = ValidateAccess(claim, EmpLevelEnum.Lead, EmpLevelEnum.Director, EmpLevelEnum.VicePresident);

            if (!resultAccess)
            {
                listFilter.Add(x => x.TblEmployees.Any(e => e.Id == claim.AccoundId));
            }

            if (!string.IsNullOrEmpty(request.ProjectTitle))
            {
                listFilter.Add(x => x.ProjectTitle.Contains(request.ProjectTitle));
            }

            if (!string.IsNullOrEmpty(request.ProjectDescription))
            {
                listFilter.Add(x => x.ProjectDescription.Contains(request.ProjectDescription));
            }

            if (request.Status != null)
            {
                listFilter.Add(x => x.Status == request.Status);
            }

            if (!string.IsNullOrEmpty(request.AssignedEmpName))
            {
                listFilter.Add(x => x.TblEmployees.Any(e => e.EmpName.Contains(request.AssignedEmpName)));
            }

            switch (request.Orderby)
            {
                case OrderByEnum.ProjectDescription:
                    sortOrderBy = x => x.ProjectDescription;
                    break;
                case OrderByEnum.ProjectTitle:
                    sortOrderBy = x => x.ProjectTitle;
                    break;
                case OrderByEnum.Status:
                    sortOrderBy = x => x.Status;
                    break;
                default:
                    sortOrderBy = x => x.CreateDate;
                    break;
            }

            if (request.IsPageList)
            {
                (projectList, totalCount) = await _projectRepo.GetAllByPaginationAsync(
                    request.PageNum,
                    request.PageSize,
                    listFilter,
                    sortOrderBy,
                    request.Asc,
                    include
                );

                totalPage = totalCount / request.PageSize;
            }
            else
            {
                (projectList, totalCount) = await _projectRepo.GetAllAsync(listFilter, sortOrderBy, request.Asc, include);

                totalPage = 1;
            }

            var resp = projectList.MapToProjectListResp();

            return ListResponse<ProjectResponse>.Add(HttpStatusCode.OK, "Success get project list", resp, totalPage, totalCount);
        }

        public async Task<ContentResponse<ProjectResponse>> GetProjectAsync(string id)
        {
            try
            {
                Expression<Func<TblProject, bool>> filter = x => x.Id == new Guid(id);
                Func<IQueryable<TblProject>, IQueryable<TblProject>> include = x => x.Include(e => e.TblEmployees);

                var project = await _projectRepo.GetAsync(filter, include);

                if (project == null)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Project not found", null);
                }

                var resp = project.MapToProjectResp();

                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.OK, "Success get the project", resp);
            }
            catch (Exception ex)
            {
                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ContentResponse<ProjectResponse>> AddProjectAsync(AddProjectRequest request, ClaimSetting claim)
        {
            try
            {
                var listFilter = new List<Expression<Func<TblEmployee, bool>>>();

                bool resultAccess = ValidateAccess(claim, EmpLevelEnum.Lead, EmpLevelEnum.Director, EmpLevelEnum.VicePresident);

                if (!resultAccess)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.Unauthorized, "Without permission to access", null);
                }

                var employees = new List<TblEmployee>();
                long totalCount = 0;

                if (request.EmployeeIds.Count != 0)
                {
                    listFilter.Add(x => request.EmployeeIds.Contains(x.Id.ToString()));

                    (employees, totalCount) = await _empRepo.GetAllAsync(listFilter);

                    if (employees.Count == 0)
                    {
                        return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Employees not found", null);
                    }
                }

                var newProject = new TblProject
                {
                    ProjectTitle = request.ProjectTitle,
                    ProjectDescription = request.ProjectDescription,
                    Status = StatusEnum.Pending,
                    TblEmployees = employees,
                    CreateBy = claim.Username
                };

                await _projectRepo.AddAsync(newProject);

                var resp = newProject.MapToProjectResp();

                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.OK, "Success add the project", resp);
            }
            catch (Exception ex)
            {
                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ContentResponse<ProjectResponse>> UpdateProjectAsync(UpdateProjectRequest request, ClaimSetting claim)
        {
            try
            {
                var listFilter = new List<Expression<Func<TblEmployee, bool>>>();

                var project = await _projectRepo.GetAsync(x => x.Id == new Guid(request.Id));

                if (project == null)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Project not found", null);
                }

                var employees = new List<TblEmployee>();
                long totalCount = 0;

                if (request.EmployeeIds.Count != 0)
                {
                    listFilter.Add(x => request.EmployeeIds.Contains(x.Id.ToString()));

                    (employees, totalCount) = await _empRepo.GetAllAsync(listFilter);

                    if (employees.Count == 0)
                    {
                        return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Employees not found", null);
                    }
                }

                bool resultAccess = ValidateAccess(claim, EmpLevelEnum.Lead, EmpLevelEnum.Director, EmpLevelEnum.VicePresident);

                if (resultAccess)
                {
                    project.ProjectTitle = request.ProjectTitle;
                    project.ProjectDescription = request.ProjectDescription;
                    project.TblEmployees = employees;
                }

                project.Status = request.Status;
                project.ModifiedBy = claim.Username;

                await _projectRepo.UpdateAsync(project);

                var resp = project.MapToProjectResp();

                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.OK, "Success update the project", resp);
            }
            catch (Exception ex)
            {
                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ContentResponse<ProjectResponse>> DeleteProjectAsync(string id)
        {
            try
            {
                var deletedProject = await _projectRepo.DeleteAsync(x => x.Id == new Guid(id));

                if (deletedProject == null)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Project not found", null);
                }

                if (deletedProject.IsEnabled)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Fail delete the project", null);
                }

                var resp = deletedProject.MapToProjectResp();

                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.OK, "Success delete the project", resp);
            }
            catch (Exception ex)
            {
                return ContentResponse<ProjectResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}
