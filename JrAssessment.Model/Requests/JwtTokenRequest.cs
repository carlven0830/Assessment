using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Requests
{
    public class JwtTokenRequest
    {
        public string AccountId { get; set; }  = string.Empty;
        public string SessionKey { get; set; } = string.Empty;
        public string Action { get; set; }     = string.Empty;
    }
}
