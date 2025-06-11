using FluentValidation;
using JrAssessment.Core.Services;
using JrAssessment.Model.Requests;
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

        [HttpPost("Add")]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request, [FromServices] IValidator<AddProjectRequest> validator)
        {
            var resultRequest = await validator.ValidateAsync(request);

            if (!resultRequest.IsValid)
            {
                return Ok(resultRequest.Errors);
            }

            var resultClaim = ValidateClaim("AddProject", out bool isValid);

            if (!isValid)
            {
                return Ok(resultClaim);
            }

            var resp = await _projectServ.AddProjectAsync(request);

            return Ok(resp);
        }
    }
}
