using JrAssessment.Core.Mapping;
using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Enums;
using JrAssessment.Model.Requests;
using JrAssessment.Model.Responses;
using JrAssessment.Model.Settings;
using JrAssessment.Repository.SqLite;
using System.Net;

namespace JrAssessment.Core.Services
{
    public interface IProjectService
    {
        Task<ContentResponse<ProjectResponse>> GetProjectAsync(string id);
        Task<ContentResponse<ProjectResponse>> AddProjectAsync(AddProjectRequest request, ClaimSetting claim);
        Task<ContentResponse<ProjectResponse>> UpdateProjectAsync(UpdateProjectRequest request, ClaimSetting claim);
        Task<ContentResponse<ProjectResponse>> DeleteProjectAsync(string id);
    }

    public class ProjectService : IProjectService
    {
        private readonly ISqLiteRepo<TblProject> _projectRepo;
        private readonly ISqLiteRepo<TblEmployee> _empRepo;

        public ProjectService(ISqLiteRepo<TblProject> projectRepo, ISqLiteRepo<TblEmployee> empRepo)
        {
            _projectRepo = projectRepo;
            _empRepo     = empRepo;
        }

        //public async Task<ListResponse<List<ProjectResponse>>> GetAllProjectAsync(FilterProjectRequest request)
        //{
        //}

        public async Task<ContentResponse<ProjectResponse>> GetProjectAsync(string id)
        {
            try
            {
                var project = await _projectRepo.GetAsync(x => x.Id == new Guid(id));

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
                var employees = new List<TblEmployee>();
                
                if (request.EmployeeIds.Count != 0)
                {
                    employees = await _empRepo.GetAllAsync(x => request.EmployeeIds.Contains(x.Id.ToString()));

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
                var project = await _projectRepo.GetAsync(x => x.Id == new Guid(request.Id));

                if (project == null)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Project not found", null);
                }

                var employees = await _empRepo.GetAllAsync(x => request.EmployeeIds.Contains(x.Id.ToString()));

                if (employees.Count == 0)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Employees not found", null);
                }

                project.ProjectTitle = request.ProjectTitle;
                project.ProjectDescription = request.ProjectDescription;
                project.TblEmployees = employees;
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
