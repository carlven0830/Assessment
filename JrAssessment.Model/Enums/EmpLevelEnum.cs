using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Model.Enums
{
    public enum EmpLevelEnum
    {
        [Display(Name = "Entry Level")]
        EntryLevel,
        Junior,
        [Display(Name = "Mid Level")]
        MidLevel,
        Senior,
        Lead,
        Director,
        [Display(Name = "Vice President")]
        VicePresident
    }
}
