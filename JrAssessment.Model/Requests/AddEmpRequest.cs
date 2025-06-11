using JrAssessment.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Requests
{
    public class AddEmpRequest : AccountRequest
    {
        public string EmpName { get; set; } = string.Empty;
        public PositionEnum EmpPosition { get; set; }
        public EmpLevelEnum EmpLevel { get; set; }
    }
}
