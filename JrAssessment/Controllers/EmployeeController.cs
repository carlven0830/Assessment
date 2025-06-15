using FluentValidation;
using JrAssessment.Core.Services;
using JrAssessment.Model.Base;
using JrAssessment.Model.Enums;
using JrAssessment.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _empServ;
        public EmployeeController(IEmployeeService empServ)
        {
            _empServ = empServ;
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> GetEmployeeList([FromQuery] ListRequest request/*, [FromServices] IValidator<ListRequest> validator*/)
        {
            //var resultRequest = await validator.ValidateAsync(request);

            //if (!resultRequest.IsValid)
            //{ 
            //    return Ok(resultRequest.Errors);
            //}

            var resultClaim = ValidateClaim(ActionEnum.GetEmployeeList.ToString(), out bool isValid);

            if (!isValid)
            {
                return Ok(resultClaim);
            }

            var resp = await _empServ.GetEmployeeListAsync(request);

            return Ok(resp);
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
