using FluentValidation;
using JrAssessment.Core.Services;
using JrAssessment.Model.Base;
using JrAssessment.Model.Enums;
using JrAssessment.Model.Requests;
using JrAssessment.Model.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectServ;

        public ProjectController(IProjectService projectServ)
        {
            _projectServ = projectServ;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetProject(string id)
        {
            var resultClaim = ValidateClaim(ActionEnum.GetProject.ToString(), out bool isValid);

            if (!isValid)
            {
                return Ok(resultClaim);
            }

            var resp = await _projectServ.GetProjectAsync(id);

            return Ok(resp);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request, [FromServices] IValidator<AddProjectRequest> validator)
        {
            var resultRequest = await validator.ValidateAsync(request);

            if (!resultRequest.IsValid)
            {
                return Ok(resultRequest.Errors);
            }

            var resultClaim = ValidateClaim(ActionEnum.AddProject.ToString(), out bool isValid);

            if (!isValid)
            {
                return Ok(resultClaim);
            }

            var resp = await _projectServ.AddProjectAsync(request, resultClaim.Content);

            return Ok(resp);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request, [FromServices] IValidator<UpdateProjectRequest> validator)
        {
            var resultRequest = await validator.ValidateAsync(request);

            if (!resultRequest.IsValid)
            {
                return Ok(resultRequest.Errors);
            }

            var resultClaim = ValidateClaim(ActionEnum.UpdateProject.ToString(), out bool isValid);

            if (!isValid)
            {
                return Ok(resultClaim);
            }

            var resp = await _projectServ.UpdateProjectAsync(request, resultClaim.Content);

            return Ok(resp);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var resultClaim = ValidateClaim(ActionEnum.DeleteProject.ToString(), out bool isValid);

            if (!isValid)
            {
                return Ok(resultClaim);
            }

            var resp = await _projectServ.DeleteProjectAsync(id);

            return Ok(resp);
        }
    }
}
