using JrAssessment.Model.Base;
using JrAssessment.Model.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace JrAssessment.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ContentResponse<ClaimSetting> ValidateClaim(string verifyAction, out bool isValid)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var id = claimsIdentity?.FindFirst("AccountId");
            var username = claimsIdentity?.FindFirst(ClaimTypes.Name);
            var action = claimsIdentity?.FindFirst("Action");

            if (id == null || username == null || action == null)
            {
                isValid = false;

                return ContentResponse<ClaimSetting>.Add(HttpStatusCode.Unauthorized, "Login first", null);
            }

            if (verifyAction != action.Value)
            {
                isValid = false;

                return ContentResponse<ClaimSetting>.Add(HttpStatusCode.Forbidden, "Invalid Action", null);
            }

            var resp = new ClaimSetting
            {
                AccoundId = new Guid(id.Value),
                Username = username.Value,
                Action = action.Value,
            };

            isValid = true;

            return ContentResponse<ClaimSetting>.Add(HttpStatusCode.OK, "Success validate claims", resp);
        }
    }
}
