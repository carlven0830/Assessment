﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Settings
{
    public class JwtTokenSetting
    {
        public string Issuer { get; set; }    = string.Empty;
        public string Audience { get; set; }  = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
