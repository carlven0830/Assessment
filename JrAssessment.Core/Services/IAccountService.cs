using JrAssessment.Core.Mapping;
using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Requests;
using JrAssessment.Model.Responses;
using JrAssessment.Model.Settings;
using JrAssessment.Repository.SqLite;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace JrAssessment.Core.Services
{
    public interface IAccountService
    {
        Task<ContentResponse<EmployeeResponse>> LoginEmployeeAsync(LoginRequest request);
        Task<ContentResponse<EmployeeResponse>> GetJwtTokenAsync(JwtTokenRequest request);
    }

    public class AccountService : IAccountService
    {
        private readonly JwtTokenSetting _jwtToken;
        private readonly ISqLiteRepo<TblEmployee> _empRepo;

        public AccountService(JwtTokenSetting jwtToken, ISqLiteRepo<TblEmployee> empRepo)
        {
            _jwtToken = jwtToken;
            _empRepo = empRepo;
        }

        public async Task<ContentResponse<EmployeeResponse>> LoginEmployeeAsync(LoginRequest request)
        {
            try
            {
                var employee = await _empRepo.GetAsync(x => x.Username == request.Username);

                if (employee == null)
                {
                    return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Username not found", null);
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, employee.Password))
                {
                    return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Password is wrong", null);
                }

                employee.SessionKey = Guid.NewGuid();

                await _empRepo.UpdateAsync(employee);

                var resp = employee.MapToEmployeeResp();

                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.OK, "Success login", resp);
            }
            catch (Exception ex)
            {
                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ContentResponse<EmployeeResponse>> GetJwtTokenAsync(JwtTokenRequest request)
        {
            try
            {
                var employee = await _empRepo.GetAsync(x => x.Id == new Guid(request.AccountId));

                if (employee == null)
                {
                    return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Employee not found", null);
                }

                if (employee.SessionKey != new Guid(request.SessionKey))
                {
                    return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.BadRequest, "Invalid session key", null);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtToken.SecretKey);

                var claims = new List<Claim>
            {
                new ("AccountId", request.AccountId.ToString()),
                new (ClaimTypes.Name, employee.Username),
                new ("Action", request.Action),
                new (ClaimTypes.Role, employee.EmpLevel.ToString())
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(30), // access token expiry
                    Audience = _jwtToken.Audience,
                    Issuer = _jwtToken.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encodedToken = tokenHandler.WriteToken(token);

                var resp = employee.MapToEmployeeResp();

                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.OK, "Success generate Jwt Token", resp, encodedToken);
            }
            catch (Exception ex)
            {
                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}
