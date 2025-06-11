using FluentValidation;
using JrAssessment.Core.Services;
using JrAssessment.Model.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accService;

        public AccountController(IAccountService accService)
        {
            _accService = accService;
        }

        [HttpPost("LoginEmployee")]
        public async Task<IActionResult> LoginEmployee([FromBody] LoginRequest request, [FromServices] IValidator<LoginRequest> validator)
        {
            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Ok(result.Errors);
            }

            var resp = await _accService.LoginEmployeeAsync(request);

            return Ok(resp);
        }

        [HttpPost("JwtToken")]
        public async Task<IActionResult> GetJwtToken([FromBody] JwtTokenRequest request, [FromServices] IValidator<JwtTokenRequest> validator)
        {
            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Ok(result.Errors);
            }

            var resp = await _accService.GetJwtTokenAsync(request);

            return Ok(resp);
        }
    }
}
