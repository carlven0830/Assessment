using JrAssessment.Model.Database;
using JrAssessment.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Responses
{
    public class EmployeeResponse : Account
    {
        public string EmpName { get; set; }     = string.Empty;
        public string EmpPosition { get; set; } = string.Empty;
        public string EmpLevel { get; set; }    = string.Empty;
    }
}
