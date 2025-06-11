using FluentValidation;
using JrAssessment.Core.Services;
using JrAssessment.Model.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empServ;
        public EmployeeController(IEmployeeService empServ)
        {
            _empServ = empServ;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmpRequest request, [FromServices] IValidator<AddEmpRequest> validator)
        {
            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return Ok(result.Errors);
            }

            var resp = await _empServ.AddEmployeeAsync(request);

            return Ok(resp);
        }


    }
}
