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
    public interface IEmployeeService
    {
        Task<ListResponse<EmployeeResponse>> GetEmployeeListAsync(ListRequest request);
        Task<ContentResponse<EmployeeResponse>> AddEmployeeAsync(AddEmpRequest request);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ISqLiteRepo<TblEmployee> _empRepo;
        public EmployeeService(ISqLiteRepo<TblEmployee> empRepo)
        {
            _empRepo = empRepo;
        }

        public async Task<ListResponse<EmployeeResponse>> GetEmployeeListAsync(ListRequest request)
        {
            try
            {
                //var listFilter = new List<Expression<Func<TblProject, bool>>>();
                //Expression<Func<TblProject, object>> sortOrderBy;
                //Func<IQueryable<TblProject>, IQueryable<TblProject>> include = x => x.Include(e => e.TblEmployees);

                var empList = new List<TblEmployee>();
                long totalCount = 0;
                long totalPage = 0;

                //switch (request.Orderby)
                //{
                //    case OrderByEnum.ProjectDescription:
                //        sortOrderBy = x => x.ProjectDescription;
                //        break;
                //    case OrderByEnum.ProjectTitle:
                //        sortOrderBy = x => x.ProjectTitle;
                //        break;
                //    case OrderByEnum.Status:
                //        sortOrderBy = x => x.Status;
                //        break;
                //    default:
                //        sortOrderBy = x => x.CreateDate;
                //        break;
                //}

                if (request.IsPageList)
                {
                    (empList, totalCount) = await _empRepo.GetAllByPaginationAsync(
                        request.PageNum,
                        request.PageSize
                    );

                    totalPage = totalCount / request.PageSize;
                }
                else
                {
                    (empList, totalCount) = await _empRepo.GetAllAsync();

                    totalPage = 1;
                }

                var resp = empList.MapToEmployeeListResp();

                return ListResponse<EmployeeResponse>.Add(HttpStatusCode.OK, "Success get employee list", resp, totalPage, totalCount);
            }
            catch (Exception ex)
            {
                return ListResponse<EmployeeResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ContentResponse<EmployeeResponse>> AddEmployeeAsync(AddEmpRequest request)
        {
            try
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
                    EmpName = request.EmpName,
                    EmpPosition = request.EmpPosition,
                    EmpLevel = request.EmpLevel,
                    Username = request.Username,
                    Password = hashPassword,
                    Email = request.Email,
                    Phone = request.Phone,
                };

                await _empRepo.AddAsync(newEmployee);

                var resp = newEmployee.MapToEmployeeResp();

                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.OK, "Success add employee account", resp);
            }
            catch (Exception ex)
            {
                return ContentResponse<EmployeeResponse>.Add(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}
