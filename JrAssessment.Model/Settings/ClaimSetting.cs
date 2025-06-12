using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Settings
{
    public class ClaimSetting
    {
        public Guid AccoundId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Action { get; set; }   = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
