using JrAssessment.Model.Enums;
using JrAssessment.Model.Settings;

namespace JrAssessment.Core.Services
{
    public interface IBaseService
    {
    }

    public class BaseService : IBaseService
    {
        protected bool ValidateAccess(ClaimSetting claim, params EmpLevelEnum[] allowedEmpLevel)
        {
            if (!allowedEmpLevel.Any(level => claim.Role == level.ToString()))
            {
                return false;
            }

            return true;
        }
    }
}
