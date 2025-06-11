using JrAssessment.Core.Mapping;
using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Requests;
using JrAssessment.Model.Responses;
using JrAssessment.Repository.SqLite;
using System.Net;

namespace JrAssessment.Core.Services
{
    public interface IEmployeeService
    {
        Task<ContentResponse<EmployeeResponse>> AddEmployeeAsync(AddEmpRequest request);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ISqLiteRepo<TblEmployee> _empRepo;
        public EmployeeService(ISqLiteRepo<TblEmployee> empRepo)
        {
            _empRepo = empRepo;
        }

        public async Task<ContentResponse<EmployeeResponse>> AddEmployeeAsync(AddEmpRequest request)
        {
            var repeatedUsername = await _empRepo.GetAsync(x => x.Username == request.Username);

            if (repeatedUsername != null)
            {
                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Username has been used", null);
            }

            var repeatedEmail = await _empRepo.GetAsync(x => x.Email == request.Email);

            if (repeatedEmail != null)
            {
                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Email has been used", null);
            }

            if (request.Password != request.ConfirmedPassword)
            {
                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Password not matched", null);
            }

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newEmployee = new TblEmployee
            {
                EmpName      = request.EmpName,
                EmpPosition  = request.EmpPosition,
                EmpLevel     = request.EmpLevel,
                Username     = request.Username,
                Password     = hashPassword,
                Email        = request.Email,
                Phone        = request.Phone,
            };

            await _empRepo.AddAsync(newEmployee);

            var resp = newEmployee.MapToEmployeeResp();

            return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.OK, "Success add employee account", resp);
        }

        //public async Task<ContentResponse<TblGuest>>
    }
}
