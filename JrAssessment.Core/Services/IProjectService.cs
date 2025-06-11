using JrAssessment.Core.Mapping;
using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Enums;
using JrAssessment.Model.Requests;
using JrAssessment.Model.Responses;
using JrAssessment.Repository.SqLite;
using System.Net;

namespace JrAssessment.Core.Services
{
    public interface IProjectService
    {
        Task<ContentResponse<ProjectResponse>> AddProjectAsync(AddProjectRequest request);
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

        public async Task<ContentResponse<ProjectResponse>> AddProjectAsync(AddProjectRequest request)
        {
            try
            {
                var employees = await _empRepo.GetAllAsync(x => request.EmployeeIds.Contains(x.Id));

                if (employees.Count == 0)
                {
                    return ContentResponse<ProjectResponse>.Add(HttpStatusCode.BadRequest, "Employees not found", null);
                }

                var newProject = new TblProject
                {
                    ProjectTitle = request.ProjectTitle,
                    ProjectDescription = request.ProjectDescription,
                    Status = StatusEnum.Pending,
                    TblEmployees = employees
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

        //public async Task<ContentResponse<RoomResponse>> UpdateRoomAsync(UpdateRoomRequest request)
        //{
        //    var room = await _roomRepo.GetAsync(x => x.Id == request.Id);

        //    if (room == null)
        //    {
        //        return ContentResponse<RoomResponse>.Add(HttpStatusCode.BadRequest, "Room not found", null);
        //    }



        //    return ContentResponse<RoomResponse>.Add(HttpStatusCode.OK, "Success update the room", resp);
        //}
    }
}
